using System;

namespace CleanArchitecture.Domain.Common
{
    public abstract class AuditableBaseEntity<TKey> : BaseEntity<TKey>
    {
        public Guid CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }

    public abstract class AuditableBaseEntity : AuditableBaseEntity<long>
    {
    }
}
