using Application.Common.Models;
using Application.TodoItems.Commands.CreateTodoItem;
using Application.TodoItems.Commands.DeleteTodoItem;
using Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers.TodoItemsControllers
{
    [ApiVersion("1.0")]
    public class TodoItemsCommandsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<BaseResult<long>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<BaseResult> Update(UpdateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<BaseResult> Delete(int id)
        {
            return await Mediator.Send(new DeleteTodoItemCommand(id));
        }
    }
}
