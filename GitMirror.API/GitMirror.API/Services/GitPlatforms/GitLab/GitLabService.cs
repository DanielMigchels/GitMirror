using GitMirror.Services.GitPlatforms.GitLab.Api;
using GitMirror.Services.GitPlatforms.Models;

namespace GitMirror.Services.GitPlatforms.GitLab;

public class GitLabService(IGitLabApiService gitLabApiService) : IGitPlatformService
{
    public GitPlatformType GitPlatformType => GitPlatformType.GitLab;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<GitRepository>> GetRepositories()
    {
        return await gitLabApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<GitRepository> CreateRepository(GitRepository repository)
    {
        return await gitLabApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
