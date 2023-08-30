using FluentValidation;
using TestTemplate.Application.Interfaces;

namespace TestTemplate.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(ITranslator translator)
        {

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithName(p => translator["Name"]);

            RuleFor(x => x.BarCode)
                .MaximumLength(50)
                .WithName(translator["BarCode"]);
        }
    }

}
