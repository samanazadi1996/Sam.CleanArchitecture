using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
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
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var endpointGroupType = typeof(EndpointGroupBase);
        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType) && !t.IsAbstract);

        foreach (var type in endpointGroupTypes)
        {
            services.AddTransient(endpointGroupType, type);
        }

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroups = app.Services.GetServices<EndpointGroupBase>();

        foreach (var instance in endpointGroups)
        {
            var groupName = instance.GroupName ?? NormalizeGroupName(instance.GetType().Name);
            var prefix = $"/api/{groupName}";

            instance.Map(app.MapGroup(prefix).WithTags(groupName));
        }

        return app;

        static string NormalizeGroupName(string endpointName)
        {
            if (string.IsNullOrWhiteSpace(endpointName))
                return string.Empty;

            return Regex.Replace(endpointName, "(Endpoints?)$", "", RegexOptions.IgnoreCase).Trim();
        }
    }

    public static RouteHandlerBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = null)
        => builder.MapGet(pattern ?? handler.Method.Name, handler);

    public static RouteHandlerBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = null)
        => builder.MapPost(pattern ?? handler.Method.Name, handler);

    public static RouteHandlerBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = null)
        => builder.MapPut(pattern ?? handler.Method.Name, handler);

    public static RouteHandlerBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = null)
        => builder.MapDelete(pattern ?? handler.Method.Name, handler);
}