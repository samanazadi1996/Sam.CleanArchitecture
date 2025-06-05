using CleanArchitecture.Application.DTOs.Account.Requests;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.Accounts.Commands.ChangeUserName;

public class ChangeUserNameCommand : ChangeUserNameRequest, IRequest<BaseResult>
{
}