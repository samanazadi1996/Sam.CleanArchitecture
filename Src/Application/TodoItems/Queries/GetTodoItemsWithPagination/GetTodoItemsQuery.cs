using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination
{
    public record GetTodoItemsQuery : IRequest<List<TodoItemDto>>
    {
    }

    public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsQuery, List<TodoItemDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItemDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
        {
            return await _context.TodoItems.AsNoTracking().Select(p => new TodoItemDto()
            {
                Id = p.Id,
                Title = p.Title.Value,
                Description = p.Description.Value,
                TimeTodo = p.TimeTodo.Value
            }).ToListAsync();
        }
    }
}
