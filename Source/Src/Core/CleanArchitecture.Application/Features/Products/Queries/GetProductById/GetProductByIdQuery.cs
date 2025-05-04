using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Products.DTOs;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<BaseResult<ProductDto>>
{
    public long Id { get; set; }
}
