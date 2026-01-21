using MediatR;
using N5.Domain;
using System;

namespace N5.Application
{
    public class CreatePermission : IRequest<Permission>
    {
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
