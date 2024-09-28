using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CleanArchitecture.Infrastructure.AuditLog.Mongo.Models;

public class MongoAuditLog
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; }
    public string EntityId { get; set; }
    public string EntityName { get; set; }
    public string EntityAssemblyName { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }

}
