using CleanArchitecture.Application.Parameters;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Products.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.GetPagedListProduct
{
    public class GetPagedListProductQuery : PagenationRequestParameter, IRequest<PagedResponse<ProductDto>>
    {
        public string Name { get; set; }
    }
}
