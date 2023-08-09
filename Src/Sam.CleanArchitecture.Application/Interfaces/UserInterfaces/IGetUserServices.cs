using Sam.CleanArchitecture.Application.DTOs.Account.Requests;
using Sam.CleanArchitecture.Application.DTOs.Account.Responses;
using Sam.CleanArchitecture.Application.Wrappers;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Application.Interfaces.UserInterfaces
{
    public interface IGetUserServices
    {
        Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model);
    }
}
