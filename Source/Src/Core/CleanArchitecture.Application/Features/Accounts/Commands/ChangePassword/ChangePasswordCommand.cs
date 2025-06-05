using CleanArchitecture.Application.DTOs.Account.Requests;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommand : ChangePasswordRequest, IRequest<BaseResult>
{
}