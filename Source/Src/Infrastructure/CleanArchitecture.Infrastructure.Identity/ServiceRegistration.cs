using CleanArchitecture.Application.Interfaces.UserInterfaces;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Infrastructure.Identity.Contexts;
using CleanArchitecture.Infrastructure.Identity.Models;
using CleanArchitecture.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure.Identity
    {
    public static class ServiceRegistration
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

            var jwtSettings = configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();
            services.AddSingleton(jwtSettings);

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(async o =>
                {

                    o.Authority = "https://accounts.google.com/";
                    o.TokenValidationParameters = new TokenValidationParameters
                        {
                        ValidateIssuer = true,
                        ValidIssuer = "https://accounts.google.com/",
                        ValidateAudience = true,
                        ValidAudiences = new List<string>
            {
                "283580482176-v7o7a3vs9sd269i8qtknjua8kddmine1.apps.googleusercontent.com", // Replace with your actual Google client ID
                // Add more audiences as needed (e.g., Facebook client ID)
            },
                        ValidateLifetime = true
                        };

                    /* Facebook configuration
                    o.Authority = "https://graph.facebook.com/";
                    o.TokenValidationParameters = new TokenValidationParameters
                        {
                        ValidateIssuer = true,
                        ValidIssuer = "https://graph.facebook.com/",
                        ValidateAudience = true,
                        ValidAudiences = new List<string>
            {
                "YOUR_FACEBOOK_APP_ID", // Replace with your actual Facebook app ID of client wasm
                // Add more audiences as needed
            },
                        ValidateLifetime = true
                        };
                    */
                    /*
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                        {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                        };
                    o.Events = new JwtBearerEvents()
                        {
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
                    */

                });
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

            }

        }
    }
