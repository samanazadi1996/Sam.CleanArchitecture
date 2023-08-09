using MediatR;
using Sam.CleanArchitecture.Application.Wrappers;

namespace Sam.CleanArchitecture.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<BaseResult>
    {
        public long Id { get; set; }
    }
}
