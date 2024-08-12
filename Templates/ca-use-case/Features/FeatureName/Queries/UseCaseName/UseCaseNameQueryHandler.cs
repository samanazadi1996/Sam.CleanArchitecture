using CleanArchitecture.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.FeatureName.Queries.UseCaseName
{
    public class UseCaseNameQueryHandler : IRequestHandler<UseCaseNameQuery, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(UseCaseNameQuery request, CancellationToken cancellationToken)
        {
            // Handler

            return request.MyProperty;
        }
    }
}
