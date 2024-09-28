using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.AuditLog.EventStore.Services;
using CleanArchitecture.Infrastructure.AuditLog.Mongo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.AuditLog;

public static class ServiceRegistration
{
    public static IServiceCollection AddAuditLogInfrastructure(this IServiceCollection services, IConfiguration configuration, AuditLogType auditLogType)
    {
        if (auditLogType == AuditLogType.Mongo)
        {
            return services.AddMongoAuditLogInfrastructure(configuration);
        }

        return services.AddEventStoreAuditLogInfrastructure(configuration);

    }
    public static IServiceCollection AddMongoAuditLogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetConnectionString("MongoAuditLogConnection")!.Split(";");

        var connection = config[0].Trim();
        var databaseName = config[1].Trim();
        var collectionName = config[2].Trim();

        services.AddSingleton<IAuditLogService>(new MongoAuditLogService(connection, databaseName, collectionName));

        return services;
    }
    public static IServiceCollection AddEventStoreAuditLogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetConnectionString("EventStoreAuditLogConnection")!.Split(";");

        var connectionString = config[0].Trim();
        var streamName = config[1].Trim();

        services.AddSingleton<IAuditLogService>(new EventStoreAuditLogService(connectionString, streamName));

        return services;
    }
}
public enum AuditLogType
{
    Mongo,
    EventStore
}
