using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class DeletePermissionTypeHandler : IRequestHandler<DeletePermissionType>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public DeletePermissionTypeHandler(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }
        public async Task Handle(DeletePermissionType request, CancellationToken cancellationToken)
        {
            await _permissionTypeRepository.DeletePermissionType(request.Id);
        }
    }
}
