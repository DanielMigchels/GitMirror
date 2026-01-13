using GitMirror.Services.GitPlatforms.GitHub.Api;
using GitMirror.Services.GitPlatforms.Models;

namespace GitMirror.Services.GitPlatforms.GitHub;

public class GitHubService(IGitHubApiService gitHubApiService) : IGitPlatformService
{
    public GitPlatformType GitPlatformType => GitPlatformType.GitHub;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<GitRepository>> GetRepositories()
    {
        return await gitHubApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<GitRepository> CreateRepository(GitRepository repository)
    {
        return await gitHubApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
