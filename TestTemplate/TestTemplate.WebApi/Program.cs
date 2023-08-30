using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;
using TestTemplate.Infrastructure.FileManager.Contexts;
using TestTemplate.Infrastructure.Identity.Contexts;
using TestTemplate.Infrastructure.Persistence.Contexts;


namespace TestTemplate.WebApi
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

                await services.GetRequiredService<IdentityContext>().Database.MigrateAsync();
                await services.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
                await services.GetRequiredService<FileManagerDbContext>().Database.MigrateAsync();

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
