using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Infrastracture.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserName = httpContextAccessor.HttpContext?.User?.Identity.Name;

        }

        public string UserId { get; }
        public string UserName { get; }
    }
}
