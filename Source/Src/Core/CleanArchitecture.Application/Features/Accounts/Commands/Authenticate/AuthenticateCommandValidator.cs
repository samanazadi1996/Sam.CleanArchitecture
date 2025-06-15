using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Accounts.Commands.Authenticate;

public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
{
    public AuthenticateCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .WithName(p => translator[nameof(p.UserName)]);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .Matches(Regexs.Password)
            .WithName(p => translator[nameof(p.Password)]);
    }
}