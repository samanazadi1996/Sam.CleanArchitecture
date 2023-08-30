using MediatR;
using TestTemplate.Application.Wrappers;

namespace TestTemplate.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<BaseResult>
    {
        public long Id { get; set; }
    }
}
