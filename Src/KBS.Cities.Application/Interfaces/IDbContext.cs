using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KBS.Cities.Application.Interfaces
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
