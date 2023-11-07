using MediatR;
using N5.Domain;
using System;

namespace N5.Application
{
    public class UpdatePermission : IRequest<Permission>
    {
        public int Id { get; set; }
        public int TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}
