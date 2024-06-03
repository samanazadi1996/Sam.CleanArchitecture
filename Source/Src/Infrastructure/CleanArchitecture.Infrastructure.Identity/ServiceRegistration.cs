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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure.Identity;

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
        services.AddTransient<IAccountServices, AccountServices>();
    }
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

        var jwtSettings = configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
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
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
                o.Events = new JwtBearerEvents()
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsJsonAsync(new BaseResult(new Error(ErrorCode.AccessDenied, "You are not Authorized")));
                    },
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsJsonAsync(new BaseResult(new Error(ErrorCode.AccessDenied, "You are not authorized to access this resource")));
                    },
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();
                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() is not true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirst("AspNet.Identity.SecurityStamp");
                        if (securityStamp is null)
                            context.Fail("This token has no secuirty stamp");

                        var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validatedUser is null)
                            context.Fail("Token secuirty stamp is not valid.");
                    },

                };
            });
    }
}
