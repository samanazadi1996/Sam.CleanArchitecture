using Application.Common.Models;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers.TodoItemsControllers
{
    [ApiVersion("1.0")]
    public class TodoItemsQueriesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<BaseResult<List<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsQuery query)
        {
            return await Mediator.Send(query);
        }

    }
}
