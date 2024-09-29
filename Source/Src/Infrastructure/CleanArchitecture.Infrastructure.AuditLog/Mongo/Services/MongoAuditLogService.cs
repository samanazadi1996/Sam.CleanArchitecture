using CleanArchitecture.Application.DTOs.AuditLog.Requests;
using CleanArchitecture.Application.DTOs.AuditLog.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.AuditLog.Mongo.Settings;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.AuditLog.Mongo.Services;
internal class MongoAuditLogService : IAuditLogService
{
    private readonly IMongoCollection<Models.MongoAuditLog> AuditLogs;
    public MongoAuditLogService(MongoSettings config)
    {
        var mongoClient = new MongoClient(config.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(config.DatabaseName);

        AuditLogs = mongoDatabase.GetCollection<Models.MongoAuditLog>(config.CollectionName);
    }

    private List<Models.MongoAuditLog> _events = [];
    public void Append<T>(string entityId, Type entityType, T oldValue, T newValue, string modifiedBy)
    {
        var model = new Models.MongoAuditLog()
        {
            ModifiedBy = modifiedBy,
            ModifiedAt = DateTime.UtcNow,
            EntityId = entityId,
            EntityName = entityType.Name,
            EntityAssemblyName = entityType.FullName!,
            OldValue = JsonSerializer.Serialize(oldValue),
            NewValue = JsonSerializer.Serialize(newValue)

        };

        _events.Add(model);
    }

    public async Task<IEnumerable<AuditLogDto>> GetAll(GetAllAuditLogsRequest request)
    {
        var query = AuditLogs.AsQueryable();

        if (!string.IsNullOrEmpty(request.EntityId))
            query = query.Where(p => p.EntityId == request.EntityId);

        if (!string.IsNullOrEmpty(request.EntityName))
            query = query.Where(p => p.EntityName == request.EntityName);

        if (!string.IsNullOrEmpty(request.EntityAssemblyName))
            query = query.Where(p => p.EntityAssemblyName == request.EntityAssemblyName);

        if (!string.IsNullOrEmpty(request.ModifiedBy))
            query = query.Where(p => p.ModifiedBy == request.ModifiedBy);

        return await query.Select(p => new AuditLogDto()
        {
            ModifiedBy = p.ModifiedBy,
            EntityId = p.EntityId,
            EntityName = p.EntityName,
            EntityAssemblyName = p.EntityAssemblyName,
            NewValue = p.NewValue,
            ModifiedAt = p.ModifiedAt,
            OldValue = p.OldValue
        }).ToListAsync();
    }

    public async Task SaveLogsAsync()
    {
        await AuditLogs.InsertManyAsync(_events);

        _events.Clear();
    }

}
