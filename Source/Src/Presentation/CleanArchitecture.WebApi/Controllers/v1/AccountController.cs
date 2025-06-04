using Asp.Versioning;
using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Features.Accounts.Commands.Authenticate;
using CleanArchitecture.Application.Features.Accounts.Commands.ChangePassword;
using CleanArchitecture.Application.Features.Accounts.Commands.ChangeUserName;
using CleanArchitecture.Application.Features.Accounts.Commands.Start;
using CleanArchitecture.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1;

[ApiVersion("1")]
public class AccountController : BaseApiController
{
    [HttpPost]
    public async Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticateCommand model)
        => await Mediator.SendAsync<AuthenticateCommand, BaseResult<AuthenticationResponse>>(model);

    [HttpPut, Authorize]
    public async Task<BaseResult> ChangeUserName(ChangeUserNameCommand model)
        => await Mediator.SendAsync<ChangeUserNameCommand, BaseResult>(model);

    [HttpPut, Authorize]
    public async Task<BaseResult> ChangePassword(ChangePasswordCommand model)
        => await Mediator.SendAsync<ChangePasswordCommand, BaseResult>(model);

    [HttpPost]
    public async Task<BaseResult<AuthenticationResponse>> Start()
        => await Mediator.SendAsync<StartCommand, BaseResult<AuthenticationResponse>>(new StartCommand());
}
