using FluentValidation;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination
{

    public class GetTodoItemsQueryValidator : AbstractValidator<GetTodoItemsQuery>
    {
        public GetTodoItemsQueryValidator()
        {
        }
    }
}