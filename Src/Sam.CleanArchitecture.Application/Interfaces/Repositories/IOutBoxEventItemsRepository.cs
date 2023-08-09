using Sam.CleanArchitecture.Domain.OutBoxEventItems.Dtos;
using Sam.CleanArchitecture.Domain.OutBoxEventItems.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IOutBoxEventItemsRepository : IGenericRepository<OutBoxEventItem>
    {
        Task<Tuple<List<OutBoxEventItemDto>, int>> GetPagedListAsync(int pageNumber, int pageSize);
    }
}
