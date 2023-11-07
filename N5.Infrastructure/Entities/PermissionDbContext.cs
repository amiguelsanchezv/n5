using Microsoft.EntityFrameworkCore;
using N5.Domain;

namespace N5.Infrastructure
{
    public class PermissionDbContext : DbContext
    {
        public PermissionDbContext(DbContextOptions<PermissionDbContext> options) : base(options)
        {
        }

        public DbSet<Permission> Permission { get; set; }
    }
}
