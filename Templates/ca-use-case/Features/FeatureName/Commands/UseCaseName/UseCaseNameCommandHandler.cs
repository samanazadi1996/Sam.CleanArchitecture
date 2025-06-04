using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.FeatureName.Commands.UseCaseName
{
    public class UseCaseNameCommandHandler : IRequestHandler<UseCaseNameCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(UseCaseNameCommand request, CancellationToken cancellationToken)
        {
            // Handler

            return request.MyProperty;
        }
    }
}