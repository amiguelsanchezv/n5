using MediatR;

namespace N5.Application
{
    public class DeletePermissionType : IRequest
    {
        public int Id { get; set; }
    }
}
