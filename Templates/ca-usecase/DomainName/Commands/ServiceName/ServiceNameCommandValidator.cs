using CleanArchitecture.Application.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Products.Commands.ServiceName
{
    public class ServiceNameCommandValidator : AbstractValidator<ServiceNameCommand>
    {
        public ServiceNameCommandValidator(ITranslator translator)
        {
            // FluentValidation
        }
    }
}