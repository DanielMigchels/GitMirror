using GitMirror.API.Data;
using GitMirror.API.Data.Enums;
using GitMirror.API.Services.OverviewService.Models;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.OverviewService;

public class OverviewService(DatabaseContext db) : IOverviewService
{
    public async Task<OverviewResponseModel> Get()
    {
        var histories = await db.Histories.ToListAsync();
        
        var queuedCount = histories.Count(h => h.State == HistoryState.Queued);
        var inProgressCount = histories.Count(h => h.State == HistoryState.InProgress);
        var successfulCount = histories.Count(h => h.State == HistoryState.Successful);
        var failedCount = histories.Count(h => h.State == HistoryState.Failed);

        var thirtyDaysAgo = DateTimeOffset.UtcNow.AddDays(-30);
        var recentHistories = histories.Where(h => h.CreatedOnUtc >= thirtyDaysAgo).ToList();
        
        var dailyActivity = recentHistories
            .GroupBy(h => h.CreatedOnUtc.Date)
            .Select(g => new DailyActivityModel
            {
                Date = g.Key.ToString("yyyy-MM-dd"),
                Successful = g.Count(h => h.State == HistoryState.Successful),
                Failed = g.Count(h => h.State == HistoryState.Failed),
                Total = g.Count()
            })
            .OrderBy(d => d.Date)
            .ToList();

        var recentHistoryList = histories
            .OrderByDescending(h => h.CreatedOnUtc)
            .Take(10)
            .Select(h => new RecentHistoryModel
            {
                Id = h.Id.ToString(),
                State = h.State.ToString(),
                SourceUrl = h.SourceUrl,
                TargetUrl = h.TargetUrl,
                CreatedOnUtc = h.CreatedOnUtc
            })
            .ToList();

        var response = new OverviewResponseModel
        {
            RepositoryCount = await db.Repositories.CountAsync(),
            MirrorCount = await db.Mirrors.CountAsync(),
            PlatformCount = await db.Platforms.CountAsync(),
            HistoryCount = histories.Count,
            QueuedCount = queuedCount,
            InProgressCount = inProgressCount,
            SuccessfulCount = successfulCount,
            FailedCount = failedCount,
            DailyActivity = dailyActivity,
            RecentHistory = recentHistoryList
        };

        return response;
    }
}
