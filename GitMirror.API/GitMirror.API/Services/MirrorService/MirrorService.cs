using GitMirror.API.Data;
using GitMirror.API.Data.Models;
using GitMirror.API.Services.MirrorService.Models;
using GitMirror.API.Services.PaginationService;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.MirrorService;

public class MirrorService(DatabaseContext db) : IMirrorService
{
    public async Task<PaginatedList<MirrorResponseModel>> Get(int pageSize, int page)
    {
        var query = db.Mirrors.AsQueryable();

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(m => new MirrorResponseModel
            {
                Id = m.Id,
                SourcePlatformId = m.SourcePlatformId,
                TargetPlatformId = m.TargetPlatformId
            })
            .ToListAsync();

        return new PaginatedList<MirrorResponseModel>
        {
            Page = page,
            PageSize = pageSize,
            HasNext = (page + 1) * pageSize < totalCount,
            HasPrevious = page > 0,
            Data = items
        };
    }

    public async Task<MirrorResponseModel?> GetById(Guid id)
    {
        var mirror = await db.Mirrors.FindAsync(id);
        
        if (mirror == null)
        {
            return null;
        }

        return new MirrorResponseModel
        {
            Id = mirror.Id,
            SourcePlatformId = mirror.SourcePlatformId,
            TargetPlatformId = mirror.TargetPlatformId
        };
    }

    public async Task<MirrorResponseModel> Create(MirrorRequestModel request)
    {
        var mirror = new Mirror
        {
            SourcePlatformId = request.SourcePlatformId,
            TargetPlatformId = request.TargetPlatformId
        };

        db.Mirrors.Add(mirror);
        await db.SaveChangesAsync();

        return new MirrorResponseModel
        {
            Id = mirror.Id,
            SourcePlatformId = mirror.SourcePlatformId,
            TargetPlatformId = mirror.TargetPlatformId
        };
    }

    public async Task<MirrorResponseModel?> Update(Guid id, MirrorRequestModel request)
    {
        var mirror = await db.Mirrors.FindAsync(id);
        
        if (mirror == null)
        {
            return null;
        }

        mirror.SourcePlatformId = request.SourcePlatformId;
        mirror.TargetPlatformId = request.TargetPlatformId;

        await db.SaveChangesAsync();

        return new MirrorResponseModel
        {
            Id = mirror.Id,
            SourcePlatformId = mirror.SourcePlatformId,
            TargetPlatformId = mirror.TargetPlatformId
        };
    }

    public async Task<bool> Delete(Guid id)
    {
        var mirror = await db.Mirrors.FindAsync(id);
        
        if (mirror == null)
        {
            return false;
        }

        db.Mirrors.Remove(mirror);
        await db.SaveChangesAsync();
        
        return true;
    }
}
