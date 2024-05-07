using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Application.Parameters;
using MediatR;

namespace CleanArchitecture.Application.Features.FeatureName.Queries.UseCaseNamePagedList
{
    public class UseCaseNamePagedListQuery : PagenationRequestParameter, IRequest<PagedResponse<string>>
    {
        public string Str { get; set; }
    }
}
