using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Application.Parameters;

namespace CleanArchitecture.Application.Features.FeatureName.Queries.UseCaseNamePagedList
{
    public class UseCaseNamePagedListQuery : PaginationRequestParameter, IRequest<PagedResponse<object>>
    {
        public object MyProperty { get; set; }
    }
}
