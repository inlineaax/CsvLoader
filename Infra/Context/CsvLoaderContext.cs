using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Infra.Context
{
    public class CsvLoaderContext : DbContext, ICsvLoaderContext
    {
        protected readonly IConfiguration Configuration;
        public CsvLoaderContext(DbContextOptions<CsvLoaderContext> options, IConfiguration configuration
            ) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        public IDbContextTransaction? Transaction { get; private set; }

        public override int SaveChanges()
        {
            var save = base.SaveChanges();
            Commit();
            return save;
        }

        private void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction.Dispose();
                Transaction = null;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Migrations
            modelBuilder.Entity<User>().HasKey(x => x.Id);

        }
    }
}
