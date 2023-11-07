using MediatR;
using N5.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class GetPermissionTypeByDescriptionHandler : IRequestHandler<GetPermissionTypeByDescription, PermissionType>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public GetPermissionTypeByDescriptionHandler(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }
        public async Task<PermissionType> Handle(GetPermissionTypeByDescription request, CancellationToken cancellationToken)
        {
            return await _permissionTypeRepository.GetPermissionTypeByDescription(request.Description);
        }
    }
}
