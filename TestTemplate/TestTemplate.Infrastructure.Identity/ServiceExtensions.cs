using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TestTemplate.Application.Interfaces.UserInterfaces;
using TestTemplate.Application.Wrappers;
using TestTemplate.Domain.Settings;
using TestTemplate.Infrastructure.Identity.Contexts;
using TestTemplate.Infrastructure.Identity.Models;
using TestTemplate.Infrastructure.Identity.Services;

namespace TestTemplate.Infrastructure.Identity
{
    public static class ServiceExtensions
    {

        public static void AddIdentityCookie(this IServiceCollection services, IConfiguration configuration)
        {
            var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
            services.AddSingleton(identitySettings);
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;

                options.Password.RequireDigit = identitySettings.PasswordRequireDigit;
                options.Password.RequiredLength = identitySettings.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = identitySettings.PasswordRequireNonAlphanumic;
                options.Password.RequireUppercase = identitySettings.PasswordRequireUppercase;
                options.Password.RequireLowercase = identitySettings.PasswordRequireLowercase;
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
        }
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("IdentityConnection"),
                b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

            services.AddTransient<IGetUserServices, GetUserServices>();
            services.AddTransient<IUpdateUserServices, UpdateUserServices>();
            services.AddTransient<IAccountServices, AccountServices>();
        }
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(async o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 401;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new BaseResult(new Error(ErrorCode.AccessDenied, "You are not Authorized")), serializerSettings);
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new BaseResult(new Error(ErrorCode.AccessDenied, "You are not authorized to access this resource")), serializerSettings);
                            return context.Response.WriteAsync(result);
                        },
                        OnTokenValidated = async context =>
                        {
                            var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();
                            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                            if (claimsIdentity.Claims?.Any() != true)
                                context.Fail("This token has no claims.");

                            var securityStamp = claimsIdentity.FindFirst("AspNet.Identity.SecurityStamp");
                            if (securityStamp is null)
                                context.Fail("This token has no secuirty stamp");

                            var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                            if (validatedUser == null)
                                context.Fail("Token secuirty stamp is not valid.");
                        },

                    };
                });

        }

    }
}
