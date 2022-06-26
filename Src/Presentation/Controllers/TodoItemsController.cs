using Application.TodoItems.Commands.CreateTodoItem;
using Application.TodoItems.Commands.DeleteTodoItem;
using Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    public class TodoItemsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateTodoItemCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoItemCommand(id));
            return Ok();
        }
    }
}
