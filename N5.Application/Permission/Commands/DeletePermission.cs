using MediatR;

namespace N5.Application
{
    public class DeletePermission : IRequest
    {
        public int Id { get; set; }
    }
}
