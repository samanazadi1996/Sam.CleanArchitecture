# [ASP Dotnet Core Clean Architecture](../README.md) - Identity Seeding

## Introduction

In ASP.NET Core applications, managing users and roles is a crucial aspect of building a secure and functional system. The Identity framework provides a robust solution for authentication, authorization, and user management. One common requirement is to seed initial users and roles into the system during the application startup. This ensures that the application starts with a predefined set of users and roles, facilitating development, testing, and initial deployment.

## Understanding Identity Seeding

Identity seeding involves populating the database with initial users and roles programmatically. This process is typically performed during application startup. By seeding initial users and roles, developers can set up the application with predefined access rights and administrative users.

## Implementing Identity Seeding in ASP.NET Core

1. Defining Roles
   - Start by defining the roles that will be used in the application. Roles represent the different levels of access or responsibilities within the system. Common roles include "Admin," "User," etc.

2. Creating Seed Data Classes
   - Create static classes to handle the seeding process. These classes will contain methods to add initial users and roles to the Identity system.

3. Seeding Roles
   - Implement a method to seed roles into the system. This method checks if the roles already exist and creates them if not. 
   - Example :
        ``` c#
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.Roles.Any() && !await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new ApplicationRole("Admin"));

                // Add other roles as needed
            }
        }
        ```

4. Seeding Users
   - Implement a method to seed initial users into the system. This method creates a default user (e.g., admin) if no users exist. 
   - Example:
        ``` c#
        public static class DefaultBasicUser
        {
            public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
            {
                //Seed Default User
                var defaultUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@Admin.com",
                    Name = "Saman",
                    PhoneNumber = "09304241296",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                if (!userManager.Users.Any())
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "Sam@12345");
                        await userManager.AddToRoleAsync(defaultUser, "Admin");
                    }

                }
            }
        }
        ```

5. Calling Seed Methods on Program.cs
   - In the Program.cs file, call the seed methods.
        ``` c#
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            //Seed Data
            await DefaultRoles.SeedAsync(services.GetRequiredService<RoleManager<ApplicationRole>>());
            await DefaultBasicUser.SeedAsync(services.GetRequiredService<UserManager<ApplicationUser>>());
        }
        ```

## Conclusion

Implementing identity seeding in ASP.NET Core applications is essential for initializing the system with initial users and roles. By following the outlined steps, developers can ensure that their applications start with predefined access rights and administrative users, streamlining the development and deployment process. Additionally, identity seeding aids in testing and ensures a consistent environment across different deployments.