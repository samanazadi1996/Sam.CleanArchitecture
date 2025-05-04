using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.WebApi.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        var requestTypes = assembly.GetTypes()
            .Where(type => type.GetInterface(typeof(IRequest<>).Name) != null);

        var handlerTypes = assembly.GetTypes()
            .Where(type => type.GetInterface(typeof(IRequestHandler<,>).Name) != null);

        foreach (var requestType in requestTypes)
        {
            var responseType = requestType.GetInterface(typeof(IRequest<>).Name).GenericTypeArguments[0];

            var handler = handlerTypes.FirstOrDefault(type =>
                type.GetInterface(typeof(IRequestHandler<,>).Name)
                    .GetGenericArguments()[0] == requestType);

            var handlerInterface = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
            services.AddTransient(handlerInterface, handler);
        }

        services.AddScoped<IMediator, MediatorService>();

        return services;
    }

}
