using System.Threading.Tasks;
using TestTemplate.Application.DTOs.Account.Requests;
using TestTemplate.Application.DTOs.Account.Responses;
using TestTemplate.Application.Wrappers;

namespace TestTemplate.Application.Interfaces.UserInterfaces
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
