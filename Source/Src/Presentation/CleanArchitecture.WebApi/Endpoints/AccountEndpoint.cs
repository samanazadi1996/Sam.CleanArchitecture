using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Features.Accounts.Commands.Authenticate;
using CleanArchitecture.Application.Features.Accounts.Commands.ChangePassword;
using CleanArchitecture.Application.Features.Accounts.Commands.ChangeUserName;
using CleanArchitecture.Application.Features.Accounts.Commands.Start;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Endpoints;

public class AccountEndpoint : EndpointGroupBase
{
    public override void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost(Authenticate);

        builder.MapPut(ChangeUserName).RequireAuthorization();

        builder.MapPut(ChangePassword).RequireAuthorization();

        builder.MapPost(Start);
    }

    async Task<BaseResult<AuthenticationResponse>> Authenticate(IMediator mediator, AuthenticateCommand model)
        => await mediator.Send<AuthenticateCommand, BaseResult<AuthenticationResponse>>(model);

    async Task<BaseResult> ChangeUserName(IMediator mediator, ChangeUserNameCommand model)
        => await mediator.Send<ChangeUserNameCommand, BaseResult>(model);

    async Task<BaseResult> ChangePassword(IMediator mediator, ChangePasswordCommand model)
        => await mediator.Send<ChangePasswordCommand, BaseResult>(model);

    async Task<BaseResult<AuthenticationResponse>> Start(IMediator mediator)
        => await mediator.Send<StartCommand, BaseResult<AuthenticationResponse>>(new StartCommand());

}
