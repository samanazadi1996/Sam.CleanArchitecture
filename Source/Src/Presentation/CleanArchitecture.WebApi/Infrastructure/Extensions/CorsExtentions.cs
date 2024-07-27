using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddAnyCors(this IServiceCollection services)
    {
        return services.AddCors(x =>
        {
            x.AddPolicy("Any", b =>
            {
                b.AllowAnyOrigin();
                b.AllowAnyHeader();
                b.AllowAnyMethod();
            });
        });
    }
    public static IApplicationBuilder UseAnyCors(this IApplicationBuilder app)
    {
        return app.UseCors("Any");
    }
}
