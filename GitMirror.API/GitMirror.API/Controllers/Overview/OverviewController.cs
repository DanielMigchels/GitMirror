using GitMirror.API.Services.OverviewService;
using Microsoft.AspNetCore.Mvc;

namespace GitMirror.API.Controllers.Overview;

[ApiController]
[Route("api/[controller]")]
public class OverviewController(IOverviewService overviewService) : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var result = await overviewService.Get();
        return Ok(result);
    }
}
