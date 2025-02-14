using CleanArchitecture.Application.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public static class RateLimitExtensions
{
    public const string AccountAuthenticate = nameof(AccountAuthenticate);
    public static IServiceCollection AddRateLimit(this IServiceCollection services)
    {
        services.AddRateLimiter(conf =>
        {
            conf.AddPolicy(AccountAuthenticate,
                httpContext => GetRateLimitPartition(httpContext, 1, TimeSpan.FromSeconds(5)));

            conf.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                context.HttpContext.Response.ContentType = "application/json";

                var contentResponse = JsonSerializer.Serialize(
                    BaseResult.Failure(new Error(ErrorCode.RateLimit, "شما به سقف محدودیت درخواست رسیدید")),
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                );

                await context.HttpContext.Response.WriteAsync(contentResponse, cancellationToken: cancellationToken);
            };
        });

        return services;

        RateLimitPartition<string> GetRateLimitPartition(HttpContext httpContext, int permitLimit, TimeSpan window)
        {
            var token = httpContext.Request.Headers["X-Forwarded-For"].ToString();

            return RateLimitPartition.GetFixedWindowLimiter(token,
                partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = permitLimit,
                    Window = window
                });
        }

        return services;
    }
    public static IApplicationBuilder UseRateLimit(this IApplicationBuilder app)
    {
        app.UseRateLimiter();

        return app;
    }
}