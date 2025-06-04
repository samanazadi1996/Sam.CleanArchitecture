using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Interfaces.UserInterfaces;
using CleanArchitecture.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Accounts.Commands.Start;

public class StartCommandHandler(IAccountServices accountServices) : IRequestHandler<StartCommand, BaseResult<AuthenticationResponse>>
{
    public async Task<BaseResult<AuthenticationResponse>> HandleAsync(StartCommand request, CancellationToken cancellationToken = default)
    {
        var ghostUsername = await accountServices.RegisterGhostAccount();
        return await accountServices.AuthenticateByUserName(ghostUsername.Data);
    }
}