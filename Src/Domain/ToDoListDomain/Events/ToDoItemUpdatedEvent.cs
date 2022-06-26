using Domain.Common;
using System;

namespace Domain.ToDoListDomain.Events
{
    public class ToDoItemUpdatedEvent : BaseEvent
    {
        public ToDoItemUpdatedEvent(string title, string description, string color, DateTime timeTodo)
        {
            Title = title;
            Description = description;
            Color = color;
            TimeTodo = timeTodo;
        }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Color { get; private set; }
        public DateTime TimeTodo { get; private set; }
    }
}
