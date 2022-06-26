using System;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination
{

    public class TodoItemDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime TimeTodo { get; set; }
    }
}
