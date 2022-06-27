using Domain.Common;
using Domain.ToDoListDomain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.EventHandlers
{
    public class TodoItemDeletedEventHandler : INotificationHandler<BaseEventDeleted<TodoItem>>
    {

        private readonly ILogger<TodoItemDeletedEventHandler> _logger;

        public TodoItemDeletedEventHandler(ILogger<TodoItemDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(BaseEventDeleted<TodoItem> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
