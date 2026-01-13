namespace GitMirror.Services.GitPlatforms;

public class GitPlatform
{
    public GitPlatformType Type { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}
