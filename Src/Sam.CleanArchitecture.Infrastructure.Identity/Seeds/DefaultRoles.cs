using System.Threading.Tasks;
using Sam.CleanArchitecture.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Sam.CleanArchitecture.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new ApplicationRole("Admin"));
        }
    }
}
