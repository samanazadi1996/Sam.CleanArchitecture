using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Interfaces.UserInterfaces;
using CleanArchitecture.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Accounts.Commands.Authenticate;

public class AuthenticateCommandHandler(IAccountServices accountServices) : IRequestHandler<AuthenticateCommand, BaseResult<AuthenticationResponse>>
{
    public async Task<BaseResult<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken = default)
    {
        return await accountServices.Authenticate(request);
    }
}