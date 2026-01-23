using GitMirror.API.Data.Enums;
using GitMirror.API.Services.PlatformIntegrationsService;

namespace GitMirror.API.Services.HistoryService.Models;

public class HistoryResponseModel
{
    public Guid Id { get; set; }
    public HistoryState State { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public Guid? MirrorId { get; set; }
    public Guid? RepositoryId { get; set; }
    public PlatformIntegrationType? SourceType { get; set; }
    public string? SourceBaseUrl { get; internal set; }
    public PlatformIntegrationType? TargetType { get; internal set; }
    public string? TargetBaseUrl { get; internal set; }
}
