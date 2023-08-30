using System.Threading.Tasks;

namespace TestTemplate.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}
