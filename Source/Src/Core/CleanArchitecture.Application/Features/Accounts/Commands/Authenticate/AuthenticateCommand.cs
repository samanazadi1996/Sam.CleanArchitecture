using CleanArchitecture.Application.DTOs.Account.Requests;
using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.Accounts.Commands.Authenticate;

public class AuthenticateCommand : AuthenticationRequest, IRequest<BaseResult<AuthenticationResponse>>
{
}