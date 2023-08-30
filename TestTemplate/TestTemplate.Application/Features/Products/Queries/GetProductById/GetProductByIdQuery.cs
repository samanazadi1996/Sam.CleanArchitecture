using MediatR;
using TestTemplate.Application.Wrappers;
using TestTemplate.Domain.Products.Dtos;

namespace TestTemplate.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<BaseResult<ProductDto>>
    {
        public long Id { get; set; }
    }
}
