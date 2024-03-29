using System;

namespace CleanArchitecture.Domain.Common
{
    public abstract class AuditableBaseEntity : BaseEntity, IAuditableEntity
    {
        public Guid CreatedBy { get; private set; }
        public DateTime Created { get; private set; }
        public Guid? LastModifiedBy { get; private set; }
        public DateTime? LastModified { get; private set; }

        void IAuditableEntity.SetCreationDetails(Guid createdBy, DateTime created)
        {
            Created = created;
            CreatedBy = createdBy;
        }

        void IAuditableEntity.SetModificationDetails(Guid? lastModifiedBy, DateTime? lastModified)
        {
            LastModified = lastModified;
            LastModifiedBy = lastModifiedBy;
        }
    }

    public interface IAuditableEntity
    {
        void SetCreationDetails(Guid createdBy, DateTime created);
        void SetModificationDetails(Guid? lastModifiedBy, DateTime? lastModified);
    }
}
