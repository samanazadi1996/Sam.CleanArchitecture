using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebApi.Infrastracture.Extensions;

public static class CorsExtentions
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
