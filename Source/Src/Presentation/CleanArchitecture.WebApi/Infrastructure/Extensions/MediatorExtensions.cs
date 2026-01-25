using CleanArchitecture.Application.Behaviours;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.WebApi.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        Assembly[] assemblies =
        {
            typeof(Application.ServiceRegistration).Assembly
        };

        services.AddMediator(assemblies)
            .AddBehavior(typeof(ValidationBehavior<,>));

        return services;
    }
    public static IServiceCollection AddBehavior(this IServiceCollection services, Type behaviorType)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), behaviorType);

        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly[] assemblies)
    {
        var requestTypes = assemblies.SelectMany(asm => asm.GetTypes())
            .Where(type => !type.IsAbstract && !type.IsInterface)
            .Select(type => new
            {
                Type = type,
                Interface = type.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
            })
            .Where(x => x.Interface != null)
            .ToList();

        var handlerTypes = assemblies.SelectMany(asm => asm.GetTypes())
            .Where(type => !type.IsAbstract && !type.IsInterface)
            .SelectMany(type => type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .Select(i => new { Handler = type, Interface = i }))
            .ToList();

        foreach (var request in requestTypes)
        {
            var responseType = request.Interface!.GetGenericArguments()[0];
            var handlerInterface = typeof(IRequestHandler<,>).MakeGenericType(request.Type, responseType);

            var handler = handlerTypes
                .FirstOrDefault(h => h.Interface == handlerInterface)?.Handler;

            if (handler != null)
            {
                services.AddTransient(handlerInterface, handler);
            }
            else
            {
                throw new InvalidOperationException($"Handler not found for request type {request.Type.FullName}");
            }
        }

        services.AddScoped<IMediator, MediatorService>();

        return services;
    }
}
