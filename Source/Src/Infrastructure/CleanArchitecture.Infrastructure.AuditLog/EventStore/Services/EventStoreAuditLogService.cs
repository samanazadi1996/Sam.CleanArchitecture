using CleanArchitecture.Application.DTOs.AuditLog.Requests;
using CleanArchitecture.Application.DTOs.AuditLog.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.AuditLog.EventStore.Models;
using CleanArchitecture.Infrastructure.AuditLog.EventStore.Settings;
using EventStore.Client;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.AuditLog.EventStore.Services;
internal class EventStoreAuditLogService : IAuditLogService
{
    private readonly string _streamName;
    private readonly EventStoreClient _client;

    public EventStoreAuditLogService(EventStoreSettings config)
    {
        var settings = EventStoreClientSettings.Create(config.ConnectionString);
        _client = new EventStoreClient(settings);
        _streamName = config.StreamName;
    }
    private List<Models.EventStoreAuditLog> _events = [];
    public void Append<T>(string entityId, Type entityType, T oldValue, T newValue, string modifiedBy)
    {
        var model = new Models.EventStoreAuditLog()
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
        var events = _client.ReadStreamAsync(Direction.Forwards, _streamName, StreamPosition.Start);

        return await (from logEntry in events.Select(resolvedEvent => resolvedEvent.Event.Data.ToArray()).Select(eventData => JsonSerializer.Deserialize<Models.EventStoreAuditLog>(eventData)).OfType<EventStoreAuditLog>()
                      where (string.IsNullOrEmpty(request.EntityId) || logEntry.EntityId == request.EntityId) && (string.IsNullOrEmpty(request.ModifiedBy) || logEntry.ModifiedBy == request.ModifiedBy) && (string.IsNullOrEmpty(request.EntityName) || logEntry.EntityName == request.EntityName) && (string.IsNullOrEmpty(request.EntityAssemblyName) || logEntry.EntityAssemblyName == request.EntityAssemblyName)
                      select new AuditLogDto
                      {
                          EntityId = logEntry.EntityId,
                          EntityName = logEntry.EntityName,
                          EntityAssemblyName = logEntry.EntityAssemblyName,
                          OldValue = logEntry.OldValue,
                          NewValue = logEntry.NewValue,
                          ModifiedBy = logEntry.ModifiedBy,
                          ModifiedAt = logEntry.ModifiedAt
                      }).ToListAsync();
    }

    public async Task SaveLogsAsync()
    {
        var eventData =
            _events.Select(item =>
                new EventData(
                    Uuid.NewUuid(),
                    item.GetType().Name,
                    JsonSerializer.SerializeToUtf8Bytes(item)));

        await _client.AppendToStreamAsync(_streamName, StreamState.Any, eventData);

        _events.Clear();
    }
}
