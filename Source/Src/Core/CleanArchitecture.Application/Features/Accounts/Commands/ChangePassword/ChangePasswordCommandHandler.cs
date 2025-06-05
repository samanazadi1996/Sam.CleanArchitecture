using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Interfaces.UserInterfaces;
using CleanArchitecture.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IAccountServices accountServices) : IRequestHandler<ChangePasswordCommand, BaseResult>
{
    public async Task<BaseResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken = default)
    {
        return await accountServices.ChangePassword(request);
    }
}