using Domain.ToDoListDomain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.EventHandlers
{
    public class TodoItemUpdatedEventHandler : INotificationHandler<ToDoItemUpdatedEvent>
    {

        private readonly ILogger<TodoItemUpdatedEventHandler> _logger;

        public TodoItemUpdatedEventHandler(ILogger<TodoItemUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ToDoItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
