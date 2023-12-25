using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTemplate.Application.DTOs;
using TestTemplate.Application.Interfaces.Repositories;
using TestTemplate.Domain.Products.Dtos;
using TestTemplate.Domain.Products.Entities;
using TestTemplate.Infrastructure.Persistence.Contexts;

namespace TestTemplate.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> products;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            products = dbContext.Set<Product>();

        }

        public async Task<PagenationResponseDto<ProductDto>> GetPagedListAsync(int pageNumber, int pageSize, string name)
        {
            var query = products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            return await Paged(
                query.Select(p => new ProductDto(p)),
                pageNumber,
                pageSize);

        }
    }
}
