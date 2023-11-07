using MediatR;
using N5.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class UpdatePermissionTypeHandler : IRequestHandler<UpdatePermissionType, PermissionType>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public UpdatePermissionTypeHandler(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }
        public async Task<PermissionType> Handle(UpdatePermissionType request, CancellationToken cancellationToken)
        {
            return await _permissionTypeRepository.UpdatePermissionType(request.Id, request.Descripcion);
        }
    }
}
