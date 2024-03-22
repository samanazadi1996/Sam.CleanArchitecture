using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Seeds
{
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
}
