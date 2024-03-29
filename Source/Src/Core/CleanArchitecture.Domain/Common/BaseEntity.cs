namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual long Id { get; private set; }
    }
}
