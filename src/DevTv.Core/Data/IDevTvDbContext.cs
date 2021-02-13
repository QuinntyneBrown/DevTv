using Microsoft.EntityFrameworkCore;
using DevTv.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Core;

namespace DevTv.Core.Data
{
    public interface IDevTvDbContext: IDbContext
    {
        DbSet<Video> Videos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
