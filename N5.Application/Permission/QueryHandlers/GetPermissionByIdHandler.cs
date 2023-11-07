using MediatR;
using N5.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class GetPermissionByIdHandler : IRequestHandler<GetPermissionById, Permission>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionByIdHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<Permission> Handle(GetPermissionById request, CancellationToken cancellationToken)
        {
            return await _permissionRepository.GetPermissionById(request.Id);
        }
    }
}
