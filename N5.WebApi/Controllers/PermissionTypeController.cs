using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using N5.Application;
using N5.Domain;
using System;
using System.Threading.Tasks;

namespace N5.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypeController : ControllerBase
    {
        private readonly IOperations _operations;
        private readonly IMediator _mediator;

        public PermissionTypeController(IOperations operations, IMediator mediator)
        {
            _operations = operations;
            _mediator = mediator;
        }

        [HttpPost(Name = "Request Permission Type", Order = 1)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> RequestPermissionType([FromBody] PermissionType permissionType)
        {
            try
            {
                return Ok(await _operations.AddPermissioType(_mediator, permissionType));
            }
            catch (Exception e)
            {
                return GetException(e);
            }
        }

        [HttpGet(Name = "Get Permission Types", Order = 1)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> GetPermissions()
        {
            try
            {
                return Ok(await _operations.GetPermissionTypes(_mediator));
            }
            catch (Exception e)
            {
                return GetException(e);
            }
        }

        private IActionResult GetException(Exception e)
        {
            return StatusCode(500, new { message = e.Message, stackTrace = e.StackTrace, innerException = e.InnerException?.Message ?? "", innerExceptionStackTrace = e.InnerException?.StackTrace ?? "" });
        }
    }
}
