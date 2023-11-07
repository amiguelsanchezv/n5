using MediatR;
using N5.Domain;

namespace N5.Application
{
    public class GetPermissionById : IRequest<Permission>
    {
        public int Id { get; set; }
    }
}
