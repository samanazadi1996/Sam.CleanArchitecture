using CleanArchitecture.Application.DTOs.Account.Requests;
using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Wrappers;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces.UserInterfaces
{
    public interface IAccountServices
    {
        Task<BaseResult<string>> RegisterGostAccount();
        Task<BaseResult> ChangePassword(ChangePasswordRequest model);
        Task<BaseResult> ChangeUserName(ChangeUserNameRequest model);
        Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
        Task<BaseResult<AuthenticationResponse>> AuthenticateByUserName(string username);

    }
}
