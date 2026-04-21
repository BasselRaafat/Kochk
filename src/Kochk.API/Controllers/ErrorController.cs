using Kochk.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Kochk.API.Controllers;

[Route("errors/{code}")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public IActionResult NotFound(int code)
    {
        return NotFound(new ApiError(code));
    }
}
