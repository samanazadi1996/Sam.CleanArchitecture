using Sam.CleanArchitecture.Application.DTOs.Account.Requests;
using Sam.CleanArchitecture.Application.DTOs.Account.Responses;
using Sam.CleanArchitecture.Application.Interfaces;
using Sam.CleanArchitecture.Application.Interfaces.UserInterfaces;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Infrastructure.Identity.Services
{
    public class GetUserServices : IGetUserServices
    {
        private readonly IdentityContext identityContext;
        private readonly IAuthenticatedUserService authenticatedUser;

        public GetUserServices(IdentityContext identityContext, IAuthenticatedUserService authenticatedUser)
        {
            this.identityContext = identityContext;
            this.authenticatedUser = authenticatedUser;
        }
        public async Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model)
        {
            var skip = (model.PageNumber - 1) * model.PageSize;

            var users = await identityContext.Users
                .Skip(skip)
                .Take(model.PageSize)
                .Select(p => new UserDto()
                {
                    Name = p.Name,
                    Email = p.Email,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    Id = p.Id,
                    Created = p.Created,
                }).ToListAsync();

            var result = Tuple.Create(users, await identityContext.Users.CountAsync());

            return new PagedResponse<UserDto>(result, model.PageNumber, model.PageSize);
        }
    }
}
