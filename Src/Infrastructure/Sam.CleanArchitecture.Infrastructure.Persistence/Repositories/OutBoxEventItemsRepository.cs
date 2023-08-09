using Sam.CleanArchitecture.Application.Interfaces.Repositories;
using Sam.CleanArchitecture.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Sam.CleanArchitecture.Domain.OutBoxEventItems.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Sam.CleanArchitecture.Domain.OutBoxEventItems.Dtos;
using System.Linq;

namespace Sam.CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class OutBoxEventItemsRepository : GenericRepository<OutBoxEventItem>, IOutBoxEventItemsRepository
    {
        private readonly DbSet<OutBoxEventItem> eventItems;

        public OutBoxEventItemsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            eventItems = dbContext.Set<OutBoxEventItem>();

        }

        public async Task<Tuple<List<OutBoxEventItemDto>, int>> GetPagedListAsync(int pageNumber, int pageSize)
        {
            var count = await eventItems.CountAsync();

            var pagedResult = await eventItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new OutBoxEventItemDto()
                {
                    Id = p.Id,
                    EventName = p.EventName,
                    EventPayload = p.EventPayload,
                    EventTypeName = p.EventTypeName
                })
                .ToListAsync();

            return new Tuple<List<OutBoxEventItemDto>, int>(pagedResult, count);

        }
    }
}
