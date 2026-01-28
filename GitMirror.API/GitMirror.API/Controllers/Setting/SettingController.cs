using GitMirror.API.Services.SettingService;
using Microsoft.AspNetCore.Mvc;

namespace GitMirror.API.Controllers.Setting;

[ApiController]
[Route("api/[controller]")]
public class SettingController(ISettingService settingService) : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var result = await settingService.Get();
        return Ok(result);
    }
}
