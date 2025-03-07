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
    
    public static IApplicationBuilder UseCustomOutputCache(this IApplicationBuilder app)
    {
        return app.UseOutputCache();
    }

    public static IServiceCollection AddCustomOutputCache(this IServiceCollection services)
    {
        return services.AddOutputCache(options =>
        {
            options.AddPolicy(Expire10Sec, builder =>
                builder.Expire(TimeSpan.FromSeconds(10)));

            options.AddPolicy(Expire1Min, builder =>
                builder.Expire(TimeSpan.FromMinutes(1)));
        });
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
