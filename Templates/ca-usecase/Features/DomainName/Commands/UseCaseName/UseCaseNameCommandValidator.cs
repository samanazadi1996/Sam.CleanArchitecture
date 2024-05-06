using CleanArchitecture.Application.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Features.DomainName.Commands.UseCaseName
{
    public class UseCaseNameCommandValidator : AbstractValidator<UseCaseNameCommand>
    {
        public UseCaseNameCommandValidator(ITranslator translator)
        {
            // FluentValidation
        }
    }
}