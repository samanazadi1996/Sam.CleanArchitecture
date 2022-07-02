using Application.Common.Interfaces;
using Hangfire;
using Infrastructure.BackgroundJobs;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

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
