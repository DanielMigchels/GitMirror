using GitMirror.API.Services.SettingService;
using GitMirror.API.Services.SettingService.Models;
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

    [HttpPut()]
    public async Task<IActionResult> Update([FromBody] SettingModel request)
    {
        var result = await settingService.Update(request);

        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }            
    }

    [HttpPost("trigger")]
    public async Task<IActionResult> Trigger([FromBody] TriggerJobRequest request)
    {
        var result = await settingService.TriggerJob(request.JobName);

        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}
