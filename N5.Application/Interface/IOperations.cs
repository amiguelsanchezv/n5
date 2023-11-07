using MediatR;
using N5.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5.Application
{
    public interface IOperations
    {
        Task<ICollection<PermissionResponse>> GetPermissions(IMediator mediator);
        Task<ICollection<PermissionType>> GetPermissionTypes(IMediator mediator);
        Task<Permission> AddPermission(IMediator mediator, Permission permission);
        Task<Permission> ModifyPermission(IMediator mediator, Permission permission);
        Task<PermissionType> AddPermissioType(IMediator mediator, PermissionType permissionType);
    }
}
