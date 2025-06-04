using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Interfaces.UserInterfaces;
using CleanArchitecture.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Accounts.Commands.ChangeUserName;

public class ChangeUserNameCommandHandler(IAccountServices accountServices) : IRequestHandler<ChangeUserNameCommand, BaseResult>
{
    public async Task<BaseResult> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken = default)
    {
        return await accountServices.ChangeUserName(request);
    }
}