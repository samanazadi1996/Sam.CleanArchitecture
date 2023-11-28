using TestTemplate.Application.Interfaces;
using TestTemplate.Application.Interfaces.UserInterfaces;
using TestTemplate.Infrastructure.Identity.Contexts;

namespace TestTemplate.Infrastructure.Identity.Services
{
    public class UpdateUserServices : IUpdateUserServices
    {
        private readonly IdentityContext identityContext;
        private readonly IAuthenticatedUserService authenticatedUser;

        public UpdateUserServices(IdentityContext identityContext, IAuthenticatedUserService authenticatedUser)
        {
            this.identityContext = identityContext;
            this.authenticatedUser = authenticatedUser;
        }
    }
}
