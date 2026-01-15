using GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api;
using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.Bitbucket;

public class BitbucketService(IBitbucketApiService bitbucketApiService) : IPlatformIntegrationService
{
    public PlatformIntegrationType GitPlatformType => PlatformIntegrationType.Bitbucket;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<PlatformIntegrationRepository>> GetRepositories()
    {
        return await bitbucketApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<PlatformIntegrationRepository> CreateRepository(PlatformIntegrationRepository repository)
    {
        return await bitbucketApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
