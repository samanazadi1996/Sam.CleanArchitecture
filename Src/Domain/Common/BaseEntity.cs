using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        private List<BaseEvent> domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => domainEvents.AsReadOnly();


        public void AddDomainEvent(BaseEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }
    }


}
