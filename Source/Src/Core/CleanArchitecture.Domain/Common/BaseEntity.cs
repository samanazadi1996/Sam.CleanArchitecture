using System;

namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
        public Guid CreatedBy { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
    public abstract class BaseEntity : BaseEntity<long>
    {
    }
}
