using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.ToDoListDomain.Entities;
using Domain.ToDoListDomain.ValueObjects;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.UpdateTodoItem
{

    public record UpdateTodoItemCommand : IRequest<BaseResult>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime TimeTodo { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, BaseResult>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResult> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
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

            await _context.PushNotifications();

            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResult();
        }
    }
}