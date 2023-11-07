using MediatR;
using N5.Domain;

namespace N5.Application
{
    public class CreatePermissionType : IRequest<PermissionType>
    {
        public string Descripcion { get; set; }
    }
}
