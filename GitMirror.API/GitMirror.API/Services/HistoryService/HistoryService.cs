using GitMirror.API.Data;
using GitMirror.API.Data.Models;
using GitMirror.API.Services.HistoryService.Models;
using GitMirror.API.Services.PaginationService;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.HistoryService;

public class HistoryService(DatabaseContext db) : IHistoryService
{
    public async Task<PaginatedList<HistoryResponseModel>> Get(int pageSize, int page)
    {
        var query = db.Histories
            .OrderByDescending(h => h.CreatedOnUtc)
            .AsQueryable();

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(h => new HistoryResponseModel
            {
                Id = h.Id,
                State = h.State,
                CreatedOnUtc = h.CreatedOnUtc,
                MirrorId = h.MirrorId,
                RepositoryId = h.RepositoryId
            })
            .ToListAsync();

        return new PaginatedList<HistoryResponseModel>
        {
            Page = page,
            PageSize = pageSize,
            HasNext = (page + 1) * pageSize < totalCount,
            HasPrevious = page > 0,
            Data = items
        };
    }

    public async Task<HistoryResponseModel?> GetById(Guid id)
    {
        var history = await db.Histories.FindAsync(id);
        
        if (history == null)
        {
            return null;
        }

        return new HistoryResponseModel
        {
            Id = history.Id,
            State = history.State,
            CreatedOnUtc = history.CreatedOnUtc,
            MirrorId = history.MirrorId,
            RepositoryId = history.RepositoryId
        };
    }

    public async Task<HistoryResponseModel> Create(HistoryRequestModel request)
    {
        var history = new History
        {
            State = request.State,
            MirrorId = request.MirrorId,
            RepositoryId = request.RepositoryId,
            CreatedOnUtc = DateTimeOffset.UtcNow
        };

        db.Histories.Add(history);
        await db.SaveChangesAsync();

        return new HistoryResponseModel
        {
            Id = history.Id,
            State = history.State,
            CreatedOnUtc = history.CreatedOnUtc,
            MirrorId = history.MirrorId,
            RepositoryId = history.RepositoryId
        };
    }

    public async Task<HistoryResponseModel?> Update(Guid id, HistoryRequestModel request)
    {
        var history = await db.Histories.FindAsync(id);
        
        if (history == null)
        {
            return null;
        }

        history.State = request.State;
        history.MirrorId = request.MirrorId;
        history.RepositoryId = request.RepositoryId;

        await db.SaveChangesAsync();

        return new HistoryResponseModel
        {
            Id = history.Id,
            State = history.State,
            CreatedOnUtc = history.CreatedOnUtc,
            MirrorId = history.MirrorId,
            RepositoryId = history.RepositoryId
        };
    }

    public async Task<bool> Delete(Guid id)
    {
        var history = await db.Histories.FindAsync(id);
        
        if (history == null)
        {
            return false;
        }

        db.Histories.Remove(history);
        await db.SaveChangesAsync();
        
        return true;
    }
}
