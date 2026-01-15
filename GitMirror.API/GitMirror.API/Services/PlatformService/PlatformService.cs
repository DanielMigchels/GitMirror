using GitMirror.API.Data;
using GitMirror.API.Services.PaginationService;
using GitMirror.API.Services.PlatformService.Models;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.PlatformService;

public class PlatformService(DatabaseContext db) : IPlatformService
{
    public async Task<PaginatedList<PlatformResponseModel>> Get(int pageSize, int page)
    {
        var query = db.Platforms.AsQueryable();

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(p => new PlatformResponseModel
            {
                Id = p.Id,
                Type = p.Type,
                Username = p.Username,
                BaseUrl = p.BaseUrl
            })
            .ToListAsync();

        return new PaginatedList<PlatformResponseModel>
        {
            Page = page,
            PageSize = pageSize,
            HasNext = (page + 1) * pageSize < totalCount,
            HasPrevious = page > 0,
            Data = items
        };
    }

    public async Task<PlatformResponseModel?> GetById(Guid id)
    {
        var platform = await db.Platforms.FindAsync(id);
        
        if (platform == null)
        {
            return null;
        }

        return new PlatformResponseModel
        {
            Id = platform.Id,
            Type = platform.Type,
            Username = platform.Username,
            BaseUrl = platform.BaseUrl
        };
    }

    public async Task<PlatformResponseModel> Create(PlatformRequestModel request)
    {
        var platform = new Data.Models.Platform
        {
            Type = request.Type,
            Username = request.Username,
            Password = request.Password,
            BaseUrl = request.BaseUrl
        };

        db.Platforms.Add(platform);
        await db.SaveChangesAsync();

        return new PlatformResponseModel
        {
            Id = platform.Id,
            Type = platform.Type,
            Username = platform.Username,
            BaseUrl = platform.BaseUrl
        };
    }

    public async Task<PlatformResponseModel?> Update(Guid id, PlatformRequestModel request)
    {
        var platform = await db.Platforms.FindAsync(id);
        
        if (platform == null)
        {
            return null;
        }

        platform.Type = request.Type;
        platform.Username = request.Username;
        platform.Password = request.Password;
        platform.BaseUrl = request.BaseUrl;

        await db.SaveChangesAsync();

        return new PlatformResponseModel
        {
            Id = platform.Id,
            Type = platform.Type,
            Username = platform.Username,
            BaseUrl = platform.BaseUrl
        };
    }

    public async Task<bool> Delete(Guid id)
    {
        var platform = await db.Platforms.FindAsync(id);
        
        if (platform == null)
        {
            return false;
        }

        db.Platforms.Remove(platform);
        await db.SaveChangesAsync();
        
        return true;
    }
}
