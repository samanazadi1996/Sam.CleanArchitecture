using Sam.CleanArchitecture.Application.Interfaces;
using Sam.CleanArchitecture.Application.Interfaces.Repositories;
using Sam.CleanArchitecture.Application.Interfaces.UserInterfaces;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Infrastructure.Identity.Services
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
