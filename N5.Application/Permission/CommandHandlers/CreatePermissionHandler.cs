using MediatR;
using N5.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermission, Permission>
    {
        private readonly IPermissionRepository _permissionRepository;

        public CreatePermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<Permission> Handle(CreatePermission request, CancellationToken cancellationToken)
        {
            return await _permissionRepository.AddPermission(new Permission()
            {
                EmployeeName = request.EmployeeName,
                EmployeeLastName = request.EmployeeLastName,
                PermissionType = request.PermissionType,
                PermissionDate = request.PermissionDate
            });
        }
    }
}
