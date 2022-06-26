using Domain.ToDoListDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<TodoItem> TodoItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
