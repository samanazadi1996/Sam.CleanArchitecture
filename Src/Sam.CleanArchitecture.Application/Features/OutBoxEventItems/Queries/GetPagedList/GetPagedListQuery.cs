using MediatR;
using Sam.CleanArchitecture.Application.Parameters;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.OutBoxEventItems.Dtos;

namespace Sam.CleanArchitecture.Application.Features.OutBoxEventItems.Queries.GetPagedList
{
    public class GetPagedListQuery : PagenationRequestParameter, IRequest<PagedResponse<OutBoxEventItemDto>>
    {
    }
}
