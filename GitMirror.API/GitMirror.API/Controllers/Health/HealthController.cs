using Microsoft.AspNetCore.Mvc;

namespace GitMirror.API.Controllers.Health;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Healthy");
    }
}
