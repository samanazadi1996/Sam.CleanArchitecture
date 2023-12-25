using System.Threading.Tasks;
using TestTemplate.Application.DTOs;
using TestTemplate.Domain.Products.Dtos;
using TestTemplate.Domain.Products.Entities;

namespace TestTemplate.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PagenationResponseDto<ProductDto>> GetPagedListAsync(int pageNumber, int pageSize, string name);
    }
}
