namespace GitMirror.API.Services.OverviewService.Models;

public class RecentHistoryModel
{
    public string Id { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public DateTimeOffset CreatedOnUtc { get; set; }
}
