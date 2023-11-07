using MediatR;
using N5.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class CreatePermissionTypeHandler : IRequestHandler<CreatePermissionType, PermissionType>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public CreatePermissionTypeHandler(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }
        public async Task<PermissionType> Handle(CreatePermissionType request, CancellationToken cancellationToken)
        {
            return await _permissionTypeRepository.AddPermissionType(new PermissionType()
            {
                Descripcion = request.Descripcion
            });
        }
    }
}
