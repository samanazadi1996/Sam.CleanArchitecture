using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Reflection;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public abstract class EndpointGroupBase
{
    public virtual string EndpointName { get; }
    public abstract void Map(RouteGroupBuilder builder);
}
public static class EndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                var endpointName = instance.EndpointName ?? instance.GetType().Name.Replace("Endpoint", "");
                var prefix = $"/api/{endpointName}";
                instance.Map(app.MapGroup(prefix));
            }
        }

        return app;
    }

    public static RouteHandlerBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler)
        => builder.MapGet(handler.Method.Name, handler);

    public static RouteHandlerBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler)
        => builder.MapPost(handler.Method.Name, handler);

    public static RouteHandlerBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler)
        => builder.MapPut(handler.Method.Name, handler);

    public static RouteHandlerBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler)
        => builder.MapDelete(handler.Method.Name, handler);

}