using GitMirror.API.Data;
using GitMirror.API.Data.Models;
using GitMirror.API.Services.PaginationService;
using GitMirror.API.Services.RepositoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.RepositoryService;

public class RepositoryService(DatabaseContext db) : IRepositoryService
{
    public async Task<PaginatedList<RepositoryResponseModel>> Get(int pageSize, int page)
    {
        var query = db.Repositories.AsQueryable();

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(r => new RepositoryResponseModel
            {
                Id = r.Id,
                SourceCloneUrl = r.SourceCloneUrl,
                SourceUsername = r.SourceUsername,
                TargetCloneUrl = r.TargetCloneUrl,
                TargetUsername = r.TargetUsername
            })
            .ToListAsync();

        return new PaginatedList<RepositoryResponseModel>
        {
            Page = page,
            PageSize = pageSize,
            HasNext = (page + 1) * pageSize < totalCount,
            HasPrevious = page > 0,
            Data = items
        };
    }

    public async Task<RepositoryResponseModel?> GetById(Guid id)
    {
        var repository = await db.Repositories.FindAsync(id);
        
        if (repository == null)
        {
            return null;
        }

        return new RepositoryResponseModel
        {
            Id = repository.Id,
            SourceCloneUrl = repository.SourceCloneUrl,
            SourceUsername = repository.SourceUsername,
            TargetCloneUrl = repository.TargetCloneUrl,
            TargetUsername = repository.TargetUsername
        };
    }

    public async Task<RepositoryResponseModel> Create(RepositoryRequestModel request)
    {
        var repository = new Repository
        {
            SourceCloneUrl = request.SourceCloneUrl,
            SourceUsername = request.SourceUsername,
            SourcePassword = request.SourcePassword,
            TargetCloneUrl = request.TargetCloneUrl,
            TargetUsername = request.TargetUsername,
            TargetPassword = request.TargetPassword
        };

        db.Repositories.Add(repository);
        await db.SaveChangesAsync();

        return new RepositoryResponseModel
        {
            Id = repository.Id,
            SourceCloneUrl = repository.SourceCloneUrl,
            SourceUsername = repository.SourceUsername,
            TargetCloneUrl = repository.TargetCloneUrl,
            TargetUsername = repository.TargetUsername
        };
    }

    public async Task<RepositoryResponseModel?> Update(Guid id, RepositoryRequestModel request)
    {
        var repository = await db.Repositories.FindAsync(id);
        
        if (repository == null)
        {
            return null;
        }

        repository.SourceCloneUrl = request.SourceCloneUrl;
        repository.SourceUsername = request.SourceUsername;
        repository.SourcePassword = request.SourcePassword;
        repository.TargetCloneUrl = request.TargetCloneUrl;
        repository.TargetUsername = request.TargetUsername;
        repository.TargetPassword = request.TargetPassword;

        await db.SaveChangesAsync();

        return new RepositoryResponseModel
        {
            Id = repository.Id,
            SourceCloneUrl = repository.SourceCloneUrl,
            SourceUsername = repository.SourceUsername,
            TargetCloneUrl = repository.TargetCloneUrl,
            TargetUsername = repository.TargetUsername
        };
    }

    public async Task<bool> Delete(Guid id)
    {
        var repository = await db.Repositories.FindAsync(id);
        
        if (repository == null)
        {
            return false;
        }

        db.Repositories.Remove(repository);
        await db.SaveChangesAsync();
        
        return true;
    }
}
