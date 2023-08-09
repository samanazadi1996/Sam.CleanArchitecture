using MediatR;
using Sam.CleanArchitecture.Application.Interfaces.Repositories;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.OutBoxEventItems.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Application.Features.OutBoxEventItems.Queries.GetPagedList
{
    public class GetPagedListQueryHandler : IRequestHandler<GetPagedListQuery, PagedResponse<OutBoxEventItemDto>>
    {
        private readonly IOutBoxEventItemsRepository  outBoxEventItemsRepository;

        public GetPagedListQueryHandler(IOutBoxEventItemsRepository outBoxEventItemsRepository)
        {
            this.outBoxEventItemsRepository = outBoxEventItemsRepository;
        }

        public async Task<PagedResponse<OutBoxEventItemDto>> Handle(GetPagedListQuery request, CancellationToken cancellationToken)
        {
            var result = await outBoxEventItemsRepository.GetPagedListAsync(request.PageNumber, request.PageSize);

            return new PagedResponse<OutBoxEventItemDto>(result, request);
        }
    }
}
