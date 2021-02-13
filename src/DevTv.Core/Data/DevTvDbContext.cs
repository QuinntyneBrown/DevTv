using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DevTv.Core.Models;

namespace DevTv.Core.Data
{
    public class DevTvDbContext: DbContext, IDevTvDbContext
    {
        public DevTvDbContext(DbContextOptions<DevTvDbContext> options)
            :base(options) { }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Video> Videos { get; private set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DevTvDbContext).Assembly);
        }
    }
}
