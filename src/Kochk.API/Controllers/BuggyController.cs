using Kochk.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Kochk.API.Controllers;

public class BuggyController : BaseApiController
{
    public BuggyController() { }

    [HttpGet("NotFound")]
    public IActionResult notFound()
    {
        return NotFound(new ApiError(404));
    }

    [HttpGet("ServerError")]
    public IActionResult ServerError()
    {
        throw new Exception();
    }

    [HttpGet("ValidationError/{id}")]
    public IActionResult ValidationError([FromRoute] int id)
    {
        return Ok(new { id, ModelState });
    }

    [HttpGet("BadRequest")]
    public IActionResult badRequest()
    {
        return BadRequest(new ApiError(400));
    }

    [HttpGet("UnAuthrized")]
    public IActionResult UnAuthrized()
    {
        return Unauthorized();
    }
}
