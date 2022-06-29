using Domain.Common;
using Domain.ToDoListDomain.Events;
using Domain.ToDoListDomain.ValueObjects;

namespace Domain.ToDoListDomain.Entities
{
    public class TodoItem : BaseAuditableEntity
    {
        public TitleValueObject Title { get; private set; }
        public DescriptionValueObject Description { get; private set; }
        public ColorValueObject Color { get; private set; }
        public TimeTodoValueObject TimeTodo { get; private set; }
    
        private TodoItem()
        {

        }

        public TodoItem(TitleValueObject title, DescriptionValueObject description, ColorValueObject color, TimeTodoValueObject timeTodo)
        {
            Title = title;
            Description = description;
            Color = color;
            TimeTodo = timeTodo;
            AddDomainEvent(new ToDoItemCreatedEvent(title.Value, description.Value, color.Value, timeTodo.Value));
        }

        public void Update(TitleValueObject title, DescriptionValueObject description, ColorValueObject color, TimeTodoValueObject timeTodo)
        {
            Title = title;
            Description = description;
            Color = color;
            TimeTodo = timeTodo;
            AddDomainEvent(new ToDoItemUpdatedEvent(title.Value, description.Value, color.Value, timeTodo.Value));
        }
    }
}
