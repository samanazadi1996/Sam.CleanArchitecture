using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sam.CleanArchitecture.Application.Interfaces;
using Sam.CleanArchitecture.Infrastructure.Resources.Services;

namespace Sam.CleanArchitecture.Infrastructure.Resources
{
    public static class ServiceRegistration
    {
        public static void AddResourcesInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddSingleton<ITranslator, Translator>();
        }
    }
}
