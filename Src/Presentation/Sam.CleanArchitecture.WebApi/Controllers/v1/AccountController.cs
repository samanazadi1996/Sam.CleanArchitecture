
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sam.CleanArchitecture.Application.DTOs.Account.Requests;
using Sam.CleanArchitecture.Application.DTOs.Account.Responses;
using Sam.CleanArchitecture.Application.Interfaces.UserInterfaces;
using Sam.CleanArchitecture.Application.Wrappers;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountServices accountServices;

        public AccountController(IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }

        [HttpPost]
        public async Task<Result<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
            => await accountServices.Authenticate(request);

        [HttpPut, Authorize]
        public async Task<BaseResult> ChangeUserName(ChangeUserNameRequest model)
            => await accountServices.ChangeUserName(model);

        [HttpPut, Authorize]
        public async Task<BaseResult> ChangePassword(ChangePasswordRequest model)
            => await accountServices.ChangePassword(model);

        [HttpPost]
        public async Task<Result<AuthenticationResponse>> Start()
        {
            var gostUsername = await accountServices.RegisterGostAccount();
            return await accountServices.AuthenticateByUserName(gostUsername.Data);
        }

    }
}