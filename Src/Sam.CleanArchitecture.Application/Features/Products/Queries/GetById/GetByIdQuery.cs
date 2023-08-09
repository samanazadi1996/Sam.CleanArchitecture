using MediatR;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.Products.Dtos;

namespace Sam.CleanArchitecture.Application.Features.Products.Queries.GetById
{
    public class GetByIdQuery : IRequest<BaseResult<ProductDto>>
    {
        public long Id { get; set; }
    }
}
