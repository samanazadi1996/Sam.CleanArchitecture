using MediatR;
using Sam.CleanArchitecture.Application.Parameters;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.Products.Dtos;

namespace Sam.CleanArchitecture.Application.Features.Products.Queries.GetPagedList
{
    public class GetPagedListQuery : PagenationRequestParameter, IRequest<PagedResponse<ProductDto>>
    {
    }
}
