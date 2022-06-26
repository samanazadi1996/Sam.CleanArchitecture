using FluentValidation;

namespace Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title).NotEmpty();

            RuleFor(v => v.Color).NotEmpty();

            RuleFor(v => v.TimeTodo).NotEmpty();
        }
    }

}