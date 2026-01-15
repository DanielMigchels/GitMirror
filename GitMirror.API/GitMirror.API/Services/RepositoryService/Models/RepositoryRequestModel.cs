namespace GitMirror.API.Services.RepositoryService.Models;

public class RepositoryRequestModel
{
    public string SourceCloneUrl { get; set; } = string.Empty;
    public string SourceUsername { get; set; } = string.Empty;
    public string SourcePassword { get; set; } = string.Empty;
    public string TargetCloneUrl { get; set; } = string.Empty;
    public string TargetUsername { get; set; } = string.Empty;
    public string TargetPassword { get; set; } = string.Empty;
}
