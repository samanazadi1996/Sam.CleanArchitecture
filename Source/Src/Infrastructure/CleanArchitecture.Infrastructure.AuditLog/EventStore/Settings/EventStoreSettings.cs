namespace CleanArchitecture.Infrastructure.AuditLog.EventStore.Settings;

internal class EventStoreSettings
{
    public string ConnectionString { get; set; }
    public string StreamName { get; set; }
}
