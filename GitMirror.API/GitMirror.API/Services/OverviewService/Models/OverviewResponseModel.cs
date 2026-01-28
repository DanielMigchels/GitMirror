namespace GitMirror.API.Services.OverviewService.Models;

public class OverviewResponseModel
{
    public int RepositoryCount { get; set; }
    public int MirrorCount { get; set; }
    public int PlatformCount { get; set; }
    public int HistoryCount { get; set; }
}
