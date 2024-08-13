using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.Resources.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Resources;

public static class ServiceRegistration
{
    public static IServiceCollection AddResourcesInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITranslator, Translator>();

        return services;
    }
}
