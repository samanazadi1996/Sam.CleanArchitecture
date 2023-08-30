using System.Threading.Tasks;
using TestTemplate.Application.DTOs.Account.Requests;
using TestTemplate.Application.DTOs.Account.Responses;
using TestTemplate.Application.Wrappers;

namespace TestTemplate.Application.Interfaces.UserInterfaces
{
    public interface IGetUserServices
    {
        Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model);
    }
}
