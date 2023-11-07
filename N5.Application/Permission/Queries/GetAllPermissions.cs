using MediatR;
using N5.Domain;
using System.Collections.Generic;

namespace N5.Application
{
    public class GetAllPermissions : IRequest<ICollection<Permission>>
    {
    }
}
