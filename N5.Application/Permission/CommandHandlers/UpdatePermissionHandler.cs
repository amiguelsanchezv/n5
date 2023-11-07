using MediatR;
using N5.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermission, Permission>
    {
        private readonly IPermissionRepository _permissionRepository;

        public UpdatePermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<Permission> Handle(UpdatePermission request, CancellationToken cancellationToken)
        {
            return await _permissionRepository.UpdatePermission(request.Id, request.TipoPermiso);
        }
    }
}
