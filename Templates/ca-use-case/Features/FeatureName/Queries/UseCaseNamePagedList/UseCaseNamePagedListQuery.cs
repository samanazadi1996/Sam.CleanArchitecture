using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Application.Parameters;
using MediatR;

namespace CleanArchitecture.Application.Features.FeatureName.Queries.UseCaseNamePagedList
{
    public class UseCaseNamePagedListQuery : PagenationRequestParameter, IRequest<PagedResponse<object>>
    {
        public object MyProperty { get; set; }
    }
}
