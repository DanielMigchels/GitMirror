using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api;
using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps;

public class AzureDevOpsService(IAzureDevOpsApiService azureDevOpsApiService) : IPlatformIntegrationService
{
    public PlatformIntegrationType GitPlatformType => PlatformIntegrationType.AzureDevOps;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<PlatformIntegrationRepository>> GetRepositories()
    {
        return await azureDevOpsApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<PlatformIntegrationRepository> CreateRepository(PlatformIntegrationRepository repository)
    {
        return await azureDevOpsApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
