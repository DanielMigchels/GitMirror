using GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api;
using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.GitLab;

public class GitLabService(IGitLabApiService gitLabApiService) : IPlatformService
{
    public PlatformType GitPlatformType => PlatformType.GitLab;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<Repository>> GetRepositories()
    {
        return await gitLabApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<Repository> CreateRepository(Repository repository)
    {
        return await gitLabApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
