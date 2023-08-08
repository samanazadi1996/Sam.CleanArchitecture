namespace Sam.CleanArchitecture.Domain.Common
{
    public class BaseEvent
    {

    }

    public class BaseEventDeleted<T> where T : BaseEntity
    {
        public BaseEventDeleted()
        {

        }
        public void SetId(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}

