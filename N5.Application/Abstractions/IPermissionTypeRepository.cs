using N5.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5.Application
{
    public interface IPermissionTypeRepository
    {
        Task<ICollection<PermissionType>> GetAll();

        Task<PermissionType> GetPermissionTypeByDescription(string description);

        Task<PermissionType> GetPermissionTypeById(int id);

        Task<PermissionType> AddPermissionType(PermissionType permissionType);

        Task<PermissionType> UpdatePermissionType(int id, string description);

        Task DeletePermissionType(int id);
    }
}
