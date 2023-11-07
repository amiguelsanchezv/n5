using MediatR;
using N5.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Application
{
    public class GetPermissionTypeByDescription : IRequest<PermissionType>
    {
        public string Description { get; set; }
    }
}
