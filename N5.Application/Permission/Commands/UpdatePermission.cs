using MediatR;
using N5.Domain;
using System;

namespace N5.Application
{
    public class UpdatePermission : IRequest<Permission>
    {
        public int Id { get; set; }
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
