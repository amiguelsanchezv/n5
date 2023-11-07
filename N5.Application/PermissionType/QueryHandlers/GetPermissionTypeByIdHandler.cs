using MediatR;
using N5.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class GetPermissionTypeByIdHandler : IRequestHandler<GetPermissionTypeById, PermissionType>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public GetPermissionTypeByIdHandler(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }
        public async Task<PermissionType> Handle(GetPermissionTypeById request, CancellationToken cancellationToken)
        {
            return await _permissionTypeRepository.GetPermissionTypeById(request.Id);
        }
    }
}
