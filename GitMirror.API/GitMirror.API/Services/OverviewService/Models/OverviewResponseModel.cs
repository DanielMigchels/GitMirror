namespace GitMirror.API.Services.OverviewService.Models;

public class OverviewResponseModel
{
    public int RepositoryCount { get; set; }
    public int MirrorCount { get; set; }
    public int PlatformCount { get; set; }
    public int HistoryCount { get; set; }
    public int QueuedCount { get; set; }
    public int InProgressCount { get; set; }
    public int SuccessfulCount { get; set; }
    public int FailedCount { get; set; }
    public List<DailyActivityModel> DailyActivity { get; set; } = new();
    public List<RecentHistoryModel> RecentHistory { get; set; } = new();
}
