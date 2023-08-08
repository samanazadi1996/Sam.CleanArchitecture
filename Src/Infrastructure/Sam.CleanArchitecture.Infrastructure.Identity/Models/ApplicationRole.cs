using System;
using Microsoft.AspNetCore.Identity;

namespace Sam.CleanArchitecture.Infrastructure.Identity.Models
{

    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name) : base(name)
        {
        }
    }
}