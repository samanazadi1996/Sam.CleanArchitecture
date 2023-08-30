using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTemplate.Application.Interfaces;
using TestTemplate.Infrastructure.FileManager.Contexts;
using TestTemplate.Infrastructure.FileManager.Services;

namespace TestTemplate.Infrastructure.FileManager
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddFileManagerInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FileManagerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("FileManagerConnection"));
            });
            services.AddScoped<IFileManagerService, FileManagerService>();
            return services;

        }
    }
}