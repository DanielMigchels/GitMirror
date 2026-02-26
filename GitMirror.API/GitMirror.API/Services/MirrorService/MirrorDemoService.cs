using GitMirror.API.Services.MirrorService.Models;
using GitMirror.API.Services.PaginationService;

namespace GitMirror.API.Services.MirrorService;

public class MirrorDemoService : IMirrorService
{
    private readonly List<MirrorResponseModel> _demoData = new()
    {
        new MirrorResponseModel
        {
            Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440201"),
            SourcePlatformId = Guid.Parse("550e8400-e29b-41d4-a716-446655440020"),
            TargetPlatformId = Guid.Parse("550e8400-e29b-41d4-a716-446655440021"),
        }
    };

    public Task<MirrorResponseModel> Create(MirrorRequestModel request)
    {
        var newMirror = new MirrorResponseModel
        {
            Id = Guid.NewGuid(),
            SourcePlatformId = request.SourcePlatformId,
            TargetPlatformId = request.TargetPlatformId
        };
        return Task.FromResult(newMirror);
    }

    public Task<bool> Delete(Guid id)
    {
        return Task.FromResult(true);
    }

    public Task<PaginatedList<MirrorResponseModel>> Get(int pageSize, int page)
    {
        var paginatedData = _demoData
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();

        return Task.FromResult(new PaginatedList<MirrorResponseModel>
        {
            Page = page,
            PageSize = pageSize,
            Data = paginatedData,
            HasNext = (page + 1) * pageSize < _demoData.Count,
            HasPrevious = page > 0
        });
    }

    public Task<MirrorResponseModel?> GetById(Guid id)
    {
        var mirror = _demoData.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(mirror);
    }

    public Task<MirrorResponseModel?> Update(Guid id, MirrorRequestModel request)
    {
        var mirror = new MirrorResponseModel
        {
            Id = id,
            SourcePlatformId = request.SourcePlatformId,
            TargetPlatformId = request.TargetPlatformId
        };
        return Task.FromResult<MirrorResponseModel?>(mirror);
    }
}
