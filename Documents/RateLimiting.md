# [ASP Dotnet Core Clean Architecture](../README.md) - Rate Limiting


## Introduction

Rate Limiting is an essential feature in API development, controlling the number of requests a client can make within a specific timeframe. It helps manage server load, improve performance, and enhance security by preventing abuse. This guide will walk you through adding Rate Limiting to your Clean Architecture project, enabling you to define rate limits per endpoint and handle excessive requests gracefully.

For a complete implementation, check out the [Rate Limiting branch on Sam.CleanArchitecture in GitHub](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/rate-limit).

## Steps to Use

### Step 1: Implement the Rate Limiting Class

Start by creating a class named `RateLimitExtensions`. This class will contain Rate Limiting configurations such as request limits and time windows and define the response for users who exceed the request limit.

```csharp
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
                options.PermitLimit = 5; // Number of allowed requests
                options.Window = TimeSpan.FromMinutes(1); // Time window
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });

            conf.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                context.HttpContext.Response.ContentType = "application/json";

                var contentResponse = JsonSerializer.Serialize(
                    BaseResult.Failure(new Error(ErrorCode.RateLimit, "Rate limit exceeded. Please try again later.")), 
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                );

                await context.HttpContext.Response.WriteAsync(contentResponse, cancellationToken: cancellationToken);
            };
        });

        return services;
    }

    public static IApplicationBuilder UseRateLimit(this IApplicationBuilder app)
    {
        app.UseRateLimiter();
        return app;
    }
}
```

In this class:
- The `AddRateLimit` method configures Rate Limiting with a 5-request limit per minute.
- If the limit is exceeded, the response includes a `429` status code and a JSON error message: `"Rate limit exceeded"`.

---

### Step 2: Register Rate Limiting in `Program.cs`

Next, register Rate Limiting within the `Program.cs` file. Use the `AddRateLimit` method to add the service to the Dependency Injection (DI) container, and `UseRateLimit` to enable it in the request pipeline.

```csharp
// Program.cs

var builder = WebApplication.CreateBuilder(args);

// Add Rate Limiting to services
builder.Services.AddRateLimit();

var app = builder.Build();

// Enable Rate Limiting in the request pipeline
app.UseRateLimit();

app.Run();
```

---

### Step 3: Apply Rate Limiting to Controller Actions

With Rate Limiting configured, you can apply it to specific controller actions by using the `[EnableRateLimiting]` attribute. Specify the rate limit configuration name as a parameter, such as `"FiveTimesInOneMinute"`.

Below is an example of applying Rate Limiting to the `Authenticate` action in `AccountController`:

```csharp
[ApiVersion("1")]
public class AccountController(IAccountServices accountServices) : BaseApiController
{
    [HttpPost]
    [EnableRateLimiting(RateLimitExtensions.FiveTimesInOneMinute)]
    public async Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
        => await accountServices.Authenticate(request);
}
```

This setup restricts the `Authenticate` action to 5 requests per minute per user. If the limit is exceeded, the response returns a `429` status code with an error message.

---

## Conclusion

By following these steps, you can seamlessly integrate Rate Limiting into your Clean Architecture project to manage incoming requests effectively. For a complete implementation example, visit the [Rate Limiting branch in the Sam.CleanArchitecture GitHub repository](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/rate-limit).
