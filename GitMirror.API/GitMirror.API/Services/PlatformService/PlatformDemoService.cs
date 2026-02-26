using GitMirror.API.Services.PaginationService;
using GitMirror.API.Services.PlatformIntegrationsService;
using GitMirror.API.Services.PlatformService.Models;

namespace GitMirror.API.Services.PlatformService;

public class PlatformDemoService : IPlatformService
{
    private readonly List<PlatformResponseModel> _demoData = new()
    {
        new PlatformResponseModel
        {
            Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440020"),
            Type = PlatformIntegrationType.AzureDevOps,
            Username = "Administrator",
            BaseUrl = "https://yourorganization.visualstudio.com/"
        },
        new PlatformResponseModel
        {
            Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440021"),
            Type = PlatformIntegrationType.GitLab,
            Username = "Administrator",
            BaseUrl = "https://gitlab.yourorganization.com/"
        }
    };

    public Task<PlatformResponseModel> Create(PlatformRequestModel request)
    {
        var newPlatform = new PlatformResponseModel
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            Username = request.Username,
            BaseUrl = request.BaseUrl
        };
        return Task.FromResult(newPlatform);
    }

    public Task<bool> Delete(Guid id)
    {
        return Task.FromResult(true);
    }

    public Task<PaginatedList<PlatformResponseModel>> Get(int pageSize, int page)
    {
        var paginatedData = _demoData
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();

        return Task.FromResult(new PaginatedList<PlatformResponseModel>
        {
            Page = page,
            PageSize = pageSize,
            Data = paginatedData,
            HasNext = (page + 1) * pageSize < _demoData.Count,
            HasPrevious = page > 0
        });
    }

    public Task<PlatformResponseModel?> GetById(Guid id)
    {
        var platform = _demoData.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(platform);
    }

    public Task<PlatformResponseModel?> Update(Guid id, PlatformRequestModel request)
    {
        var platform = new PlatformResponseModel
        {
            Id = id,
            Type = request.Type,
            Username = request.Username,
            BaseUrl = request.BaseUrl
        };
        return Task.FromResult<PlatformResponseModel?>(platform);
    }
}
