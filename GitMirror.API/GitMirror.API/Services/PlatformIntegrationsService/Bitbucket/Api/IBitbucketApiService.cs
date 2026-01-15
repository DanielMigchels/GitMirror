using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api;

public interface IBitbucketApiService
{
    Task<PlatformIntegrationRepository> CreateRepository(string baseUrl, string username, string password, PlatformIntegrationRepository repository);
    Task<List<PlatformIntegrationRepository>> GetRepositories(string baseUrl, string username, string password);
}
