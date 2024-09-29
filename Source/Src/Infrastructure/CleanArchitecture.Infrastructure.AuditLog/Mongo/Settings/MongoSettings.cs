namespace CleanArchitecture.Infrastructure.AuditLog.Mongo.Settings;

internal class MongoSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
