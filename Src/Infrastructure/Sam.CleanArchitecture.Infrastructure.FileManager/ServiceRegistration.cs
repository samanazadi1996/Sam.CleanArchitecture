using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sam.CleanArchitecture.Application.Interfaces;
using Sam.CleanArchitecture.Infrastructure.FileManager.Contexts;
using Sam.CleanArchitecture.Infrastructure.FileManager.Services;

namespace Sam.CleanArchitecture.Infrastructure.FileManager
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