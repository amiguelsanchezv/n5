using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class DeletePermissionHandler : IRequestHandler<DeletePermission>
    {
        private readonly IPermissionRepository _permissionRepository;

        public DeletePermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task Handle(DeletePermission request, CancellationToken cancellationToken)
        {
            await _permissionRepository.DeletePermission(request.Id);
        }
    }
}
