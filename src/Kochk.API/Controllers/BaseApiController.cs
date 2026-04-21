using Kochk.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kochk.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected ActionResult HandleError(Error error)
    {
        var errorResponse = new { error.Id, error.Description };
        return error.Type switch
        {
            ErrorType.NotFound => NotFound(errorResponse),
            ErrorType.Validation => BadRequest(errorResponse),
            ErrorType.Unauthorized => Unauthorized(errorResponse),
            _ => StatusCode(500, errorResponse),
        };
    }
}
