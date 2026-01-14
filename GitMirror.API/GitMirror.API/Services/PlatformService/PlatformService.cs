using GitMirror.API.Data;
using GitMirror.API.Data.Models;
using GitMirror.API.Services.PaginationService;
using GitMirror.API.Services.PlatformService.Models;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.PlatformService;

public class PlatformService(DatabaseContext db) : IPlatformService
{
    public async Task<PaginatedList<PlatformResponseModel>> Get(int pageSize = 2147483647, int page = 0)
    {
        var data = await db.Platforms.AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(x => new PlatformResponseModel()
            {
                Id = x.Id,
                Username = x.Username,
                BaseUrl = x.BaseUrl,
                Type = x.Type,
            })
            .ToListAsync();

        return new PaginatedList<PlatformResponseModel>()
        {
            Data = data,
            HasNext = await db.Platforms.Skip((page + 1) * pageSize).AnyAsync(),
            HasPrevious = page > 0,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<PlatformResponseModel?> GetById(Guid id)
    {
        var platform = await db.Platforms.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new PlatformResponseModel()
            {
                Id = x.Id,
                Username = x.Username,
                BaseUrl = x.BaseUrl,
                Type = x.Type,
            })
            .FirstOrDefaultAsync();

        return platform;
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
        var platform = await db.Platforms.FirstOrDefaultAsync(x => x.Id == id);
        
        if (platform == null)
            return null;

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
        var platform = await db.Platforms.FirstOrDefaultAsync(x => x.Id == id);
        
        if (platform == null)
            return false;

        db.Platforms.Remove(platform);
        await db.SaveChangesAsync();

        return true;
    }
}
