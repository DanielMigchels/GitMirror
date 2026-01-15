namespace GitMirror.API.Services.RepositoryService.Models;

public class RepositoryResponseModel
{
    public Guid Id { get; set; }
    public string SourceCloneUrl { get; set; } = string.Empty;
    public string SourceUsername { get; set; } = string.Empty;
    public string TargetCloneUrl { get; set; } = string.Empty;
    public string TargetUsername { get; set; } = string.Empty;
}
