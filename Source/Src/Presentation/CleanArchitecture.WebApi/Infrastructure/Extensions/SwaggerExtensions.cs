using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public static class SwaggerExtensions
{
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        const string swaggerLoginUrl = "/swagger/swagger-login.js";
        const string swaggerLoginFile = "Assets/swagger/swagger-login.js"; 
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.InjectJavascript(swaggerLoginUrl);
        });

        app.Map(swaggerLoginUrl, builder =>
        {
            builder.Run(async context =>
            {
                context.Response.ContentType = "application/javascript";
                var fileContent = await System.IO.File.ReadAllTextAsync(swaggerLoginFile);
                await context.Response.WriteAsync(fileContent);
            });
        });

        return app;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
            });
            setup.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        });

        return services;
    }
}