using GitMirror.API.Data.Enums;

namespace GitMirror.API.Services.HistoryService.Models;

public class HistoryResponseModel
{
    public Guid Id { get; set; }
    public HistoryState State { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public Guid? MirrorId { get; set; }
    public Guid? RepositoryId { get; set; }
    public string? SourceUrl { get; set; }
    public string? TargetUrl { get; set; }
}
