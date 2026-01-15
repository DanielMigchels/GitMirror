using GitMirror.API.Services.MirrorService;
using GitMirror.API.Services.MirrorService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitMirror.API.Controllers.Mirror;

[ApiController]
[Route("api/[controller]")]
public class MirrorController(IMirrorService mirrorService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMirrors([FromQuery] int pageSize = 20, [FromQuery] int page = 0)
    {
        var result = await mirrorService.Get(pageSize, page);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mirrorService.GetById(id);
        
        if (result == null)
        {
            return NotFound();
        }            

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MirrorRequestModel request)
    {
        var result = await mirrorService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MirrorRequestModel request)
    {
        var result = await mirrorService.Update(id, request);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await mirrorService.Delete(id);
        
        if (!success)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
