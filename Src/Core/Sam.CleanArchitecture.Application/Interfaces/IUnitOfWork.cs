using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}
