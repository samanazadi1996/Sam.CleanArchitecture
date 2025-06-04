using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.Accounts.Commands.Start;

public class StartCommand : IRequest<BaseResult<AuthenticationResponse>>
{
}