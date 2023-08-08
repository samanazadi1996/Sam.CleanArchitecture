using FluentValidation;
using Sam.CleanArchitecture.Application.Behaviours;

namespace Sam.CleanArchitecture.Application.DTOs.Account.Requests
{
    public class ChangeUserNameRequest
    {
        public string UserName { get; set; }
    }
    public class ChangeUserNameRequestValidator : AbstractValidator<ChangeUserNameRequest>
    {
        public ChangeUserNameRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().NotNull()
                .MinimumLength(4)
                .Matches(Regexs.UserName);
        }
    }
}
