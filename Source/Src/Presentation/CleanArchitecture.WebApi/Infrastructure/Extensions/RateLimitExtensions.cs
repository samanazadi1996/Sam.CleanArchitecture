using CleanArchitecture.Application.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public static class RateLimitExtensions
{
    public const string FiveTimesInOneMinute = nameof(FiveTimesInOneMinute);
    public static IServiceCollection AddRateLimit(this IServiceCollection services)
    {
        services.AddRateLimiter(conf =>
            {
                conf.AddFixedWindowLimiter(FiveTimesInOneMinute, options =>
                {
                    options.PermitLimit = 5;
                    options.Window = TimeSpan.FromMinutes(1);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                
                conf.OnRejected = async (context, cancellationToken) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    context.HttpContext.Response.ContentType = "application/json";

                    var contentResponse = JsonSerializer.Serialize(
                        BaseResult.Failure(new Error(ErrorCode.RateLimit, "Rate limit exceeded. Please try again later.")), new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });

                    await context.HttpContext.Response.WriteAsync(contentResponse, cancellationToken: cancellationToken);
                };
            }
          );


        return services;
    }
    public static IApplicationBuilder UseRateLimit(this IApplicationBuilder app)
    {
        app.UseRateLimiter();

        return app;
    }
}