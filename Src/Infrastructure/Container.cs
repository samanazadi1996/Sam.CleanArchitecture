using Hangfire;
using Infrastructure.BackgroundJobs;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Container
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();

            return services;
        }
        public static IApplicationBuilder UseInfrastructureServices(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<ReminderHostedService>(x => x.Execute(), Cron.Minutely());

            return app;
        }
    }
}
