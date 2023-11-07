using MediatR;
using N5.Domain;

namespace N5.Application
{
    public class GetPermissionTypeById : IRequest<PermissionType>
    {
        public int Id { get; set; }
    }
}
