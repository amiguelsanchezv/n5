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
    public class PermissionController : ControllerBase
    {
        private readonly IOperations _operations;
        private readonly IMediator _mediator;

        public PermissionController(IOperations operations, IMediator mediator)
        {
            _operations = operations;
            _mediator = mediator;
        }

        [HttpGet(Name = "Get Permissions", Order = 1)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> GetPermissions()
        {
            try
            {
                return Ok(await _operations.GetPermissions(_mediator));
            }
            catch (Exception e)
            {
                return GetException(e);
            }
        }

        [HttpPost(Name = "Request Permission", Order = 2)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> RequestPermission([FromBody] Permission permission)
        {
            try
            {
                return Ok(await _operations.AddPermission(_mediator, permission));
            }
            catch (Exception e)
            {
                return GetException(e);
            }
        }

        [HttpPut(Name = "Modify Permission", Order = 3)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> ModifyPermission([FromBody] Permission permission)
        {
            try
            {
                return Ok(await _operations.ModifyPermission(_mediator, permission));
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
