using Application.Common.Exceptions;
using Domain.ToDoListDomain.Entities;
using Domain.ToDoListDomain.ValueObjects;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.UpdateTodoItem
{

    public record UpdateTodoItemCommand : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime TimeTodo { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.Update(new TitleValueObject(request.Title),
                new DescriptionValueObject(request.Description),
                new ColorValueObject(request.Color),
                new TimeTodoValueObject(request.TimeTodo)
                );

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}