using Sam.CleanArchitecture.Application.DTOs.Account.Requests;
using Sam.CleanArchitecture.Application.Wrappers;
using System.Threading.Tasks;
using Sam.CleanArchitecture.Application.DTOs.Account.Responses;

namespace Sam.CleanArchitecture.Application.Interfaces.UserInterfaces
{
    public interface IAccountServices
    {
        Task<Result<string>> RegisterGostAccount();
        Task<BaseResult> ChangePassword(ChangePasswordRequest model);
        Task<BaseResult> ChangeUserName(ChangeUserNameRequest model);
        Task<Result<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
        Task<Result<AuthenticationResponse>> AuthenticateByUserName(string username);

    }
}
