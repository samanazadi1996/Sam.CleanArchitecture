using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.FeatureName.Queries.UseCaseNamePagedList
{
    public class UseCaseNamePagedListQueryHandler : IRequestHandler<UseCaseNamePagedListQuery, PagedResponse<object>>
    {
        public async Task<PagedResponse<object>> Handle(UseCaseNamePagedListQuery request, CancellationToken cancellationToken)
        {
            // Handler

            List<object> data = [];
            int totalCount = 100;

            var result = new PagenationResponseDto<object>(data, totalCount, request.PageNumber, request.PageSize);
            return new PagedResponse<object>(result);
        }
    }
}