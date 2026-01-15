using GitMirror.API.Services.HistoryService;
using GitMirror.API.Services.HistoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitMirror.API.Controllers.History;

[ApiController]
[Route("api/[controller]")]
public class HistoryController(IHistoryService historyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHistories([FromQuery] int pageSize = 20, [FromQuery] int page = 0)
    {
        var result = await historyService.Get(pageSize, page);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await historyService.GetById(id);
        
        if (result == null)
        {
            return NotFound();
        }            

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HistoryRequestModel request)
    {
        var result = await historyService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] HistoryRequestModel request)
    {
        var result = await historyService.Update(id, request);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await historyService.Delete(id);
        
        if (!success)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
