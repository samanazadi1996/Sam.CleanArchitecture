using CleanArchitecture.Application.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Features.FeatureName.Commands.UseCaseName
{
    public class UseCaseNameCommandValidator : AbstractValidator<UseCaseNameCommand>
    {
        public UseCaseNameCommandValidator(ITranslator translator)
        {
            RuleFor(p => p.MyProperty)
                .NotNull()
                .WithName(p => translator[nameof(p.MyProperty)]);
        }
    }
}