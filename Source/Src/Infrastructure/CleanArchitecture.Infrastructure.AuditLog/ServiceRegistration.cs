using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.AuditLog.EventStore.Services;
using CleanArchitecture.Infrastructure.AuditLog.EventStore.Settings;
using CleanArchitecture.Infrastructure.AuditLog.Mongo.Services;
using CleanArchitecture.Infrastructure.AuditLog.Mongo.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.AuditLog;

public static class ServiceRegistration
{
    public static IServiceCollection AddAuditLogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var writeTo = configuration["AuditLog:WriteTo"]!;

        if (writeTo.Equals("Mongo", StringComparison.OrdinalIgnoreCase))
        {
            var mongoSettings = configuration.GetSection("AuditLog:Mongo").Get<MongoSettings>();
            services.AddSingleton<IAuditLogService>(new MongoAuditLogService(mongoSettings!));
        }
        else if (writeTo.Equals("EventStore", StringComparison.OrdinalIgnoreCase))
        {
            var eventStoreSettings = configuration.GetSection("AuditLog:EventStore").Get<EventStoreSettings>();
            services.AddSingleton<IAuditLogService>(new EventStoreAuditLogService(eventStoreSettings!));
        }

        return services;
    }
}