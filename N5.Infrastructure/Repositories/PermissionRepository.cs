using Microsoft.EntityFrameworkCore;
using N5.Application;
using N5.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5.Infrastructure
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly PermissionDbContext _context;

        public PermissionRepository(PermissionDbContext context)
        {
            _context = context;
        }

        public async Task<Permission> AddPermission(Permission permission)
        {
            _context.Permission.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task DeletePermission(int id)
        {
            var Permission = _context.Permission.FirstOrDefault(p => p.Id == id);
            if (Permission is null) return;
            _context.Permission.Remove(Permission);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Permission>> GetAll()
        {
            return await _context.Permission.ToListAsync();
        }

        public async Task<Permission> GetPermissionById(int id)
        {
            return await _context.Permission.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Permission> UpdatePermission(int id, int permissionType)
        {
            var Permission = await _context.Permission.FirstOrDefaultAsync(p => p.Id == id);
            Permission.TipoPermiso = permissionType;
            await _context.SaveChangesAsync();
            return await _context.Permission.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
