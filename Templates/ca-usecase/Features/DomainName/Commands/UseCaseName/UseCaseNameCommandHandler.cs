using CleanArchitecture.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.DomainName.Commands.UseCaseName
{
    public class UseCaseNameCommandHandler : IRequestHandler<UseCaseNameCommand, BaseResult<long>>
    {
        public async Task<BaseResult<long>> Handle(UseCaseNameCommand request, CancellationToken cancellationToken)
        {
            // Handler

            return new BaseResult<long>(1);
        }
    }
}