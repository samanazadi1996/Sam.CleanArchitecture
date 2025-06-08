using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Parameters;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Products.DTOs;

namespace CleanArchitecture.Application.Features.Products.Queries.GetPagedListProduct;

public class GetPagedListProductQuery : PaginationRequestParameter, IRequest<PagedResponse<ProductDto>>
{
    public string Name { get; set; }
}
