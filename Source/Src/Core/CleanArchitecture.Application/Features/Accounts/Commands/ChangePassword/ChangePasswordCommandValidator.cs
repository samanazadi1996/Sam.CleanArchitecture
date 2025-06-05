using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6)
            .Matches(Regexs.Password)
            .WithName(p => translator[nameof(p.Password)]);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .Matches(Regexs.Password)
            .WithName(p => translator[nameof(p.ConfirmPassword)]);

    }
}
