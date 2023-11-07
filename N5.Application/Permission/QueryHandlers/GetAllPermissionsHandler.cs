using MediatR;
using N5.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class GetAllPermissionsHandler : IRequestHandler<GetAllPermissions, ICollection<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetAllPermissionsHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<ICollection<Permission>> Handle(GetAllPermissions request, CancellationToken cancellationToken)
        {
            return await _permissionRepository.GetAll();
        }
    }
}
