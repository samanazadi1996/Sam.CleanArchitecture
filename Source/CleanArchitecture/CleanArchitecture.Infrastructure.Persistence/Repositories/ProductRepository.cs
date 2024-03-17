using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Domain.Products.Dtos;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
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
            var query = products.OrderBy(p => p.Created).AsQueryable();

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
