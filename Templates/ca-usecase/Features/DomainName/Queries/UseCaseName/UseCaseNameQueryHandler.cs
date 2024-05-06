using CleanArchitecture.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.DomainName.Queries.UseCaseName
{
    public class UseCaseNameQueryHandler : IRequestHandler<UseCaseNameQuery, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(UseCaseNameQuery request, CancellationToken cancellationToken)
        {
            // Handler

            return new BaseResult<string>(request.Str);
        }
    }
}
