# [ASP Dotnet Core Clean Architecture](../README.md) - Localization

## Introduction

In ASP.NET Core, creating multilingual applications is essential to provide users the flexibility to interact in their preferred language. This documentation explains the implementation of localization settings using two methods: 'AddCustomLocalization' and 'UseCustomLocalization'.

## AddCustomLocalization - Localization Settings

This method is used to add localization settings to the `IServiceCollection`.

```c#
public static class LocalizationExtentions
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        var supportedCultures = configuration.GetSection("Localization:SupportedCultures")
            .Get<List<string>>().Select(p => new CultureInfo(p)).ToArray();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(configuration["Localization:DefaultRequestCulture"]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        return services;
    }

    //..
}
```

Execution Steps:

1. **Read Configuration Settings**

   - Localization settings are read from the `appsettings.json` file.
     ```json
     "Localization": {
         "DefaultRequestCulture": "en",
         "SupportedCultures": [ "en", "fa" ]
     }
     ```

2. **Configure RequestLocalization**
   - Using `Configure<RequestLocalizationOptions>` settings for default language and supported languages are applied.
   - 
3. **Return IServiceCollection**
   - The method returns `IServiceCollection` with the applied localization settings as an extension.

## UseCustomLocalization - Applying Localization Settings

This method is used to apply localization settings to the `IApplicationBuilder`.

```c#
public static class LocalizationExtentions
{
    //..

    public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app)
    {
        app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

        return app;
    }
}
```

Execution Steps:

1. **Use Localization Settings**

   - The localization settings are applied to the `RequestLocalizationMiddleware`.

2. **Return IApplicationBuilder**
   - The method returns `IApplicationBuilder`, enabling the use of localization settings at the application level.

## Usage in `Program.cs`

```c#
// ...
builder.Services.AddCustomLocalization(builder.Configuration);
// ...

// ...
app.UseCustomLocalization();
// ...
```

## Adding Dynamic Translation with [GoogleTranslator](./Localization.GoogleTranslator.md)
To enhance the localization capabilities of your application, you can incorporate dynamic translation using the GoogleTranslator. This allows your application to fetch translations dynamically, providing even greater flexibility.

## Summary

By utilizing these two methods, you can add and implement localization settings in your ASP.NET Core applications. This capability allows you to easily configure your application to display content in various languages, enhancing the overall user experience.
