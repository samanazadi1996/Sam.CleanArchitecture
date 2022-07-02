using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.ToDoListDomain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.DeleteTodoItem
{
    public record DeleteTodoItemCommand(long Id) : IRequest<BaseResult>;

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, BaseResult>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResult> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            _context.TodoItems.Remove(entity);
            
            await _context.PushNotifications();
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResult();
        }
    }

}