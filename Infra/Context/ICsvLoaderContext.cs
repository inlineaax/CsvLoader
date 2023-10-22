using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public interface ICsvLoaderContext
    {
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
