using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService;

public interface IPlatformIntegrationService
{
    public PlatformIntegrationType GitPlatformType { get; }
    string Username { get; set; }
    string Password { get; set; }
    string BaseUrl { get; set; }

    public Task<List<PlatformIntegrationRepository>> GetRepositories();
    public Task<PlatformIntegrationRepository> CreateRepository(PlatformIntegrationRepository repository);
}
