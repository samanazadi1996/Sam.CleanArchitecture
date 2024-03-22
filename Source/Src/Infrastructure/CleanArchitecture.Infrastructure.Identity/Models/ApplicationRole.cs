using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArchitecture.Infrastructure.Identity.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name) : base(name)
        {
        }
    }
}