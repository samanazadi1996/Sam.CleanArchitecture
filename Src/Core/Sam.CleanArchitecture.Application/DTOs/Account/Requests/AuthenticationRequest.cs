using FluentValidation;
using Sam.CleanArchitecture.Application.Behaviours;
using Sam.CleanArchitecture.Application.Interfaces;

namespace Sam.CleanArchitecture.Application.DTOs.Account.Requests
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator(ITranslator translator)
        {
            RuleFor(x => x.UserName)
                .NotEmpty().NotNull().WithName(translator["UserName"]);

            RuleFor(x => x.Password)
                .NotEmpty().NotNull()
                .Matches(Regexs.Password).WithName(translator["Password"]);
        }
    }
}
