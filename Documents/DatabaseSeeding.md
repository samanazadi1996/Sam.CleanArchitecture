# [ASP Dotnet Core Clean Architecture](../README.md) - Database Seeding

## Introduction

In software development, it's common to initialize databases with default data to ensure that the application starts with a predefined set of information. In this article, we'll explore how to seed default product data into the database using CleanArchitecture, a robust architectural pattern for building modern applications.

## Understanding Default Data Seeding

Default data seeding involves populating the database with initial data during application startup. This data can include various entities such as products, categories, users, etc. Seeding default products is particularly useful in scenarios where applications require predefined items for users to interact with.

## Implementing Default Data Seeding in CleanArchitecture

1. Defining Product Entity
   - Start by defining the product entity. This entity represents the core data structure for products in the application.

2. Creating Seed Data Class
   - Implement a static class to handle the seeding process. This class will contain a method to add default products to the database.

3. Seeding Default Products
   - Write a method to seed default products into the database. This method checks if any products exist and adds them if the database is empty.
   - Example :
        ``` c#
        public static class DefaultData
        {
            public static async Task SeedAsync(ApplicationDbContext applicationDbContext)
            {
                if (!await applicationDbContext.Products.AnyAsync())
                {
                    List<Product> defaultProducts = [
                        new Product("Product 1",100000,"111111111111"),
                        new Product("Product 2",150000,"222222222222"),
                        new Product("Product 3",200000,"333333333333"),
                        new Product("Product 4",105000,"444444444444"),
                        new Product("Product 5",200000,"555555555555")
                        ];

                    await applicationDbContext.Products.AddRangeAsync(defaultProducts);

                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }
        ```

4. Calling Seed Methods on Program.cs
   - In the Program.cs file, call the seed methods.
   - Example:
        ``` c#
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            //Seed Data
            await DefaultData.SeedAsync(services.GetRequiredService<ApplicationDbContext>());
        }
        ```

## Conclusion

Seeding default product data is an essential aspect of application development, ensuring that the database starts with predefined items. By following the steps outlined in this article, developers can easily seed default products into the database using CleanArchitecture. This approach streamlines the development process and provides a consistent environment for testing and deployment.