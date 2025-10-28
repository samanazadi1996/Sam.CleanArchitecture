using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public abstract class EndpointGroupBase
{
    public virtual string GroupName { get; }
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
                var groupName = instance.GroupName ?? NormalizeGroupName(instance.GetType().Name);
                var prefix = $"/api/{groupName}";
                instance.Map(app.MapGroup(prefix).WithTags(groupName));
            }
        }

        return app;
    }

    #region --- Default route naming ---
    public static RouteHandlerBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler)
        => builder.MapGet(NormalizeGroupName(handler.Method.Name), handler);

    public static RouteHandlerBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler)
        => builder.MapPost(NormalizeGroupName(handler.Method.Name), handler);

    public static RouteHandlerBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler)
        => builder.MapPut(NormalizeGroupName(handler.Method.Name), handler);

    public static RouteHandlerBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler)
        => builder.MapDelete(NormalizeGroupName(handler.Method.Name), handler);
    #endregion

    #region --- Overloads with custom route template ---
    public static RouteHandlerBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler, string pattern)
        => builder.MapGet(pattern, handler);

    public static RouteHandlerBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler, string pattern)
        => builder.MapPost(pattern, handler);

    public static RouteHandlerBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler, string pattern)
        => builder.MapPut(pattern, handler);

    public static RouteHandlerBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler, string pattern)
        => builder.MapDelete(pattern, handler);
    #endregion

    private static string NormalizeGroupName(string endpointName)
    {
        if (string.IsNullOrWhiteSpace(endpointName))
            return string.Empty;

        return Regex.Replace(endpointName, "(Endpoints?)$", "", RegexOptions.IgnoreCase).Trim();
    }
}