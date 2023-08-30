using MediatR;
using TestTemplate.Application.Parameters;
using TestTemplate.Application.Wrappers;
using TestTemplate.Domain.Products.Dtos;

namespace TestTemplate.Application.Features.Products.Queries.GetPagedListProduct
{
    public class GetPagedListProductQuery : PagenationRequestParameter, IRequest<PagedResponse<ProductDto>>
    {
        public string Name { get; set; }
    }
}
