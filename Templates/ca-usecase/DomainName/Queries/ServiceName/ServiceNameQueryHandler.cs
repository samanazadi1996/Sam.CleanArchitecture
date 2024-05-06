using CleanArchitecture.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.DomainName.Queries.ServiceName
{
    public class ServiceNameQueryHandler : IRequestHandler<ServiceNameQuery, BaseResult>
    {
        public async Task<BaseResult> Handle(ServiceNameQuery request, CancellationToken cancellationToken)
        {
            // Handler

            return new BaseResult();
        }
    }

}
