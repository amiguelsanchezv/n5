using MediatR;
using N5.Domain;
using System;

namespace N5.Application
{
    public class CreatePermission : IRequest<Permission>
    {
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}
