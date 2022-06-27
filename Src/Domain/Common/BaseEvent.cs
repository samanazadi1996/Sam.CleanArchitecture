using MediatR;

namespace Domain.Common
{
    public abstract class BaseEvent : INotification
    {

    }
    public class BaseEventDeleted<T> : INotification where T : BaseEntity
    {
        public BaseEventDeleted()
        {

        }
        public void SetId(long id)
        {
            Id = id;
        }
        public long Id { get; private set; }
    }
}
