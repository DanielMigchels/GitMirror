using GitMirror.API.Services.RepositoryService;
using GitMirror.API.Services.RepositoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitMirror.API.Controllers.Repository;

[ApiController]
[Route("api/[controller]")]
public class RepositoryController(IRepositoryService repositoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRepositories([FromQuery] int pageSize = 20, [FromQuery] int page = 0)
    {
        var result = await repositoryService.Get(pageSize, page);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await repositoryService.GetById(id);
        
        if (result == null)
        {
            return NotFound();
        }            

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RepositoryRequestModel request)
    {
        var result = await repositoryService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] RepositoryRequestModel request)
    {
        var result = await repositoryService.Update(id, request);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await repositoryService.Delete(id);
        
        if (!success)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
