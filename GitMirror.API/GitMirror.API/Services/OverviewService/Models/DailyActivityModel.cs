namespace GitMirror.API.Services.OverviewService.Models;

public class DailyActivityModel
{
    public string Date { get; set; } = string.Empty;
    public int Successful { get; set; }
    public int Failed { get; set; }
    public int Total { get; set; }
}
