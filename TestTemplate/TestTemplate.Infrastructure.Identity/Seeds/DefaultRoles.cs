using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TestTemplate.Infrastructure.Identity.Models;

namespace TestTemplate.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new ApplicationRole("Admin"));
        }
    }
}
