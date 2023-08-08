using Sam.CleanArchitecture.Domain.Common;

namespace Sam.CleanArchitecture.Domain.OutBoxEventItems.Entities
{
    public class OutBoxEventItem : AuditableBaseEntity
    {
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public string EventPayload { get; set; }
    }
}
