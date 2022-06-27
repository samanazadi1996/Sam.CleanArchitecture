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
        public async Task<ActionResult<List<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
