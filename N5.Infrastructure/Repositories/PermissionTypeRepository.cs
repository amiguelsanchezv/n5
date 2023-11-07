using Microsoft.EntityFrameworkCore;
using N5.Application;
using N5.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5.Infrastructure
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly PermissionTypeDbContext _context;

        public PermissionTypeRepository(PermissionTypeDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionType> AddPermissionType(PermissionType permissionType)
        {
            var _permissionType = await GetPermissionTypeByDescription(permissionType.Descripcion);
            if (_permissionType != null)
            {
                return _permissionType;
            }
            _context.PermissionType.Add(permissionType);
            await _context.SaveChangesAsync();
            return permissionType;
        }

        public async Task DeletePermissionType(int id)
        {
            var PermissionType = _context.PermissionType.FirstOrDefault(p => p.Id == id);
            if (PermissionType is null) return;
            _context.PermissionType.Remove(PermissionType);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<PermissionType>> GetAll()
        {
            return await _context.PermissionType.ToListAsync();
        }

        public async Task<PermissionType> GetPermissionTypeByDescription(string description)
        {
            return await _context.PermissionType.FirstOrDefaultAsync(p => p.Descripcion == description);
        }

        public async Task<PermissionType> GetPermissionTypeById(int id)
        {
            return await _context.PermissionType.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PermissionType> UpdatePermissionType(int id, string description)
        {
            var PermissionType = await _context.PermissionType.FirstOrDefaultAsync(p => p.Id == id);
            PermissionType.Descripcion = description;
            await _context.SaveChangesAsync();
            return await _context.PermissionType.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
