using FluentValidation;

namespace Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title).NotEmpty();

            RuleFor(v => v.Color).NotEmpty();

            RuleFor(v => v.TimeTodo).NotEmpty();
        }
    }

}