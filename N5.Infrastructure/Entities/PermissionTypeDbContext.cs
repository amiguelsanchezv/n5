using Microsoft.EntityFrameworkCore;
using N5.Domain;

namespace N5.Infrastructure
{
    public class PermissionTypeDbContext : DbContext
    {
        public PermissionTypeDbContext(DbContextOptions<PermissionTypeDbContext> options) : base(options)
        {
        }

        public DbSet<PermissionType> PermissionType { get; set; }
    }
}
