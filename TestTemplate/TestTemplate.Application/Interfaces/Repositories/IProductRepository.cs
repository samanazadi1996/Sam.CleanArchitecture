using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTemplate.Domain.Products.Dtos;
using TestTemplate.Domain.Products.Entities;

namespace TestTemplate.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Tuple<List<ProductDto>, int>> GetPagedListAsync(int pageNumber, int pageSize, string name);
    }
}
