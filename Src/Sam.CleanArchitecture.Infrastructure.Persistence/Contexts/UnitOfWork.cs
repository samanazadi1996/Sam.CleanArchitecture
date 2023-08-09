using Sam.CleanArchitecture.Application.Interfaces;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Infrastructure.Persistence.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
