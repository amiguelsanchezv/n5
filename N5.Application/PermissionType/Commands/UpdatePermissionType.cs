using MediatR;
using N5.Domain;

namespace N5.Application
{
    public class UpdatePermissionType : IRequest<PermissionType>
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
