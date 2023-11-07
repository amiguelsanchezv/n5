using N5.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5.Application
{
    public interface IPermissionRepository
    {
        Task<ICollection<Permission>> GetAll();

        Task<Permission> GetPermissionById(int id);

        Task<Permission> AddPermission(Permission permission);

        Task<Permission> UpdatePermission(int id, int permissionType);

        Task DeletePermission(int id);
    }
}
