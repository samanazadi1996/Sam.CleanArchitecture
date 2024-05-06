using CleanArchitecture.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.DomainName.Queries.UseCaseName
{
    public class UseCaseNameQueryHandler : IRequestHandler<UseCaseNameQuery, BaseResult>
    {
        public async Task<BaseResult> Handle(UseCaseNameQuery request, CancellationToken cancellationToken)
        {
            // Handler

            return new BaseResult();
        }
    }

}
