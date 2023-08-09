using Sam.CleanArchitecture.Domain.Products.Dtos;
using Sam.CleanArchitecture.Domain.Products.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Tuple<List<ProductDto>, int>> GetPagedListAsync(int pageNumber, int pageSize, string name);
    }
}
