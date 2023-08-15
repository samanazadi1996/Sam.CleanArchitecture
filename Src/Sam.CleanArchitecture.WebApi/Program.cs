using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sam.CleanArchitecture.Infrastructure.FileManager.Contexts;
using Sam.CleanArchitecture.Infrastructure.Identity.Contexts;
using Sam.CleanArchitecture.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                services.GetRequiredService<IdentityContext>().Database.Migrate();
                services.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                services.GetRequiredService<FileManagerDbContext>().Database.Migrate();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
