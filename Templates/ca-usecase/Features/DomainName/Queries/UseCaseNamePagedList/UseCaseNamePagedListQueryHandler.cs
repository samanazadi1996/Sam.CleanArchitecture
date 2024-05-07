using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.DomainName.Queries.UseCaseNamePagedList
{
    public class UseCaseNamePagedListQueryHandler : IRequestHandler<UseCaseNamePagedListQuery, PagedResponse<string>>
    {
        public async Task<PagedResponse<string>> Handle(UseCaseNamePagedListQuery request, CancellationToken cancellationToken)
        {
            // Handler

            List<string> data = ["data 1", "data 2", "data 3"];
            int totalCount = 100;

            var result = new PagenationResponseDto<string>(data, totalCount, request.PageNumber, request.PageSize);
            return new PagedResponse<string>(result);
        }
    }
}