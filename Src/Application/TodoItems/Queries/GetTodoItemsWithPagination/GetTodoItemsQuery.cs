using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination
{
    public record GetTodoItemsQuery : IRequest<BaseResult<List<TodoItemDto>>>
    {
    }

    public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsQuery, BaseResult<List<TodoItemDto>>>
    {
        private readonly IApplicationDbContext _context;

        public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResult<List<TodoItemDto>>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
        {
            var result = new BaseResult<List<TodoItemDto>>();

            result.Ok(await _context.TodoItems.AsNoTracking().Select(p => new TodoItemDto()
            {
                Id = p.Id,
                Title = p.Title.Value,
                Description = p.Description.Value,
                TimeTodo = p.TimeTodo.Value
            }).ToListAsync());

            return result;
        }
    }
}
