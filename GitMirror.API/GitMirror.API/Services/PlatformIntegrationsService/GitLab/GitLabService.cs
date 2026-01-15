using GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api;
using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.GitLab;

public class GitLabService(IGitLabApiService gitLabApiService) : IPlatformIntegrationService
{
    public PlatformIntegrationType GitPlatformType => PlatformIntegrationType.GitLab;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<PlatformIntegrationRepository>> GetRepositories()
    {
        return await gitLabApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<PlatformIntegrationRepository> CreateRepository(PlatformIntegrationRepository repository)
    {
        return await gitLabApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
