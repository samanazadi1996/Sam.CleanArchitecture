using CleanArchitecture.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Commands.ServiceName
{
    public class ServiceNameCommandHandler : IRequestHandler<ServiceNameCommand, BaseResult<long>>
    {
        public async Task<BaseResult<long>> Handle(ServiceNameCommand request, CancellationToken cancellationToken)
        {
            // Handler

            return new BaseResult<long>(1);
        }
    }
}