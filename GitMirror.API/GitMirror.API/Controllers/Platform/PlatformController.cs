using GitMirror.API.Services.PlatformService;
using GitMirror.API.Services.PlatformService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitMirror.API.Controllers.Platform;

[ApiController]
[Route("api/[controller]")]
public class PlatformController(IPlatformService platformService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProjects([FromQuery] int pageSize = 20, [FromQuery] int page = 0)
    {
        var result = await platformService.Get(pageSize, page);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await platformService.GetById(id);
        
        if (result == null)
        {
            return NotFound();
        }            

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlatformRequestModel request)
    {
        var result = await platformService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PlatformRequestModel request)
    {
        var result = await platformService.Update(id, request);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await platformService.Delete(id);
        
        if (!success)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
