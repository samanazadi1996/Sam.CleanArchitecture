using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Threading.Tasks;
using TestTemplate.Application.Interfaces;
using TestTemplate.Application.Interfaces.Repositories;
using TestTemplate.Application.Interfaces.UserInterfaces;
using TestTemplate.Application.Wrappers;
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
