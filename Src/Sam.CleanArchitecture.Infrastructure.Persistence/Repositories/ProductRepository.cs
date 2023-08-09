using Microsoft.EntityFrameworkCore;
using Sam.CleanArchitecture.Application.Interfaces.Repositories;
using Sam.CleanArchitecture.Domain.Products.Dtos;
using Sam.CleanArchitecture.Domain.Products.Entities;
using Sam.CleanArchitecture.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> products;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            products = dbContext.Set<Product>();

        }

        public async Task<Tuple<List<ProductDto>, int>> GetPagedListAsync(int pageNumber, int pageSize)
        {
            var count = await products.CountAsync();

            var pagedResult = await products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto(p))
                .ToListAsync();

            return new Tuple<List<ProductDto>, int>(pagedResult, count);

        }
    }
}
