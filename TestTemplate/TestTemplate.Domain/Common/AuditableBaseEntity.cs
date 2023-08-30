using System;

namespace TestTemplate.Domain.Common
{
    public abstract class AuditableBaseEntity : BaseEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
