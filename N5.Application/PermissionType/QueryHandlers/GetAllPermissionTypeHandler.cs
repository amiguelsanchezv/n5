using MediatR;
using N5.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Application
{
    public class GetAllPermissionTypeHandler : IRequestHandler<GetAllPermissionTypes, ICollection<PermissionType>>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public GetAllPermissionTypeHandler(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }
        public async Task<ICollection<PermissionType>> Handle(GetAllPermissionTypes request, CancellationToken cancellationToken)
        {
            return await _permissionTypeRepository.GetAll();
        }
    }
}
