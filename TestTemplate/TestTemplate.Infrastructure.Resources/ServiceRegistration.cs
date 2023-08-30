using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTemplate.Application.Interfaces;
using TestTemplate.Infrastructure.Resources.Services;

namespace TestTemplate.Infrastructure.Resources
{
    public static class ServiceRegistration
    {
        public static void AddResourcesInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddSingleton<ITranslator, Translator>();
        }
    }
}
