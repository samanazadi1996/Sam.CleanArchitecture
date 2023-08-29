using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sam.CleanArchitecture.Infrastructure.FileManager.Contexts;
using Sam.CleanArchitecture.Infrastructure.Identity.Contexts;
using Sam.CleanArchitecture.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;
using Serilog;
using System;
using Microsoft.Extensions.Configuration;

namespace Sam.CleanArchitecture.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            ConfigureLogging();

            var host = CreateHostBuilder(args).UseSerilog().Build();

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
        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

    }
}
