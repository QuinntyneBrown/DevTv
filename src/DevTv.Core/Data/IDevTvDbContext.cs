using Microsoft.EntityFrameworkCore;
using DevTv.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DevTv.Core.Data
{
    public interface IDevTvDbContext
    {
        DbSet<Video> Videos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
