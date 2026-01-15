using GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api;
using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.GitHub;

public class GitHubService(IGitHubApiService gitHubApiService) : IPlatformIntegrationService
{
    public PlatformIntegrationType GitPlatformType => PlatformIntegrationType.GitHub;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<PlatformIntegrationRepository>> GetRepositories()
    {
        return await gitHubApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<PlatformIntegrationRepository> CreateRepository(PlatformIntegrationRepository repository)
    {
        return await gitHubApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
