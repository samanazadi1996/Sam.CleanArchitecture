# [ASP Dotnet Core Clean Architecture](../README.md) - OutputCache

## Introduction

This article explains how to add and configure OutputCache in a Clean Architecture-based project. Using OutputCache improves application performance by serving repeated responses from the cache, reducing the need for reprocessing.

## Implementation Steps

1. Adding an Extension for OutputCache
  - First, create a static class named OutputCacheExtensions and define the necessary methods for adding and configuring OutputCache:

```c#
public static class OutputCacheExtensions
{
    internal const string Expire10Sec = nameof(Expire10Sec);
    internal const string Expire1Min = nameof(Expire1Min);
    public static IServiceCollection AddCustomOutputCache(this IServiceCollection services)
    {
        return services.AddOutputCache(options =>
        {
            options.AddPolicy(Expire10Sec, builder =>
                builder.Expire(TimeSpan.FromSeconds(10)).AddPolicy<AuthCachePolicy>());

            options.AddPolicy(Expire1Min, builder =>
                builder.Expire(TimeSpan.FromMinutes(1)).AddPolicy<AuthCachePolicy>());
        });
    }

    public static IApplicationBuilder UseCustomOutputCache(this IApplicationBuilder app)
    {
        return app.UseOutputCache();
    }

    internal sealed class AuthCachePolicy : IOutputCachePolicy
    {

        /// <inheritdoc />
        ValueTask IOutputCachePolicy.CacheRequestAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            var attemptOutputCaching = AttemptOutputCaching(context);
            context.EnableOutputCaching = true;
            context.AllowCacheLookup = attemptOutputCaching;
            context.AllowCacheStorage = attemptOutputCaching;
            context.AllowLocking = true;

            // Vary by any query by default
            context.CacheVaryByRules.QueryKeys = "*";

            return ValueTask.CompletedTask;
        }

        /// <inheritdoc />
        ValueTask IOutputCachePolicy.ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        /// <inheritdoc />
        ValueTask IOutputCachePolicy.ServeResponseAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            var response = context.HttpContext.Response;

            // Verify existence of cookie headers
            if (!StringValues.IsNullOrEmpty(response.Headers.SetCookie))
            {
                context.AllowCacheStorage = false;
                return ValueTask.CompletedTask;
            }

            // Check response code
            if (response.StatusCode != StatusCodes.Status200OK)
            {
                context.AllowCacheStorage = false;
                return ValueTask.CompletedTask;
            }

            return ValueTask.CompletedTask;
        }

        private static bool AttemptOutputCaching(OutputCacheContext context)
        {
            // Check if the current request fulfills the requirements to be cached

            var request = context.HttpContext.Request;

            // Verify the method
            return HttpMethods.IsGet(request.Method) || HttpMethods.IsHead(request.Method);
        }
    }
}
```

2. Registering OutputCache in the Application
  - In the `Program.cs` file, call the defined methods to enable OutputCache:

```c#
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomOutputCache();

var app = builder.Build();

app.UseCustomOutputCache();

app.Run();
```
3. Using OutputCache in Controllers
  - To cache the output of an action, use the [OutputCache] filter with the defined policy name from OutputCacheExtensions:

```c#
[ApiController]
[Route("api/[controller]")]
public class SampleController : ControllerBase
{
    [HttpGet]
    [OutputCache(PolicyName = OutputCacheExtensions.Expire10Sec)]
    public IActionResult GetData()
    {
        return Ok(new { Time = DateTime.Now });
    }
}
```

## Conclusion

By implementing these steps, the output of certain controller actions will be cached and expire at the specified time. This approach improves API performance and reduces server processing load.
