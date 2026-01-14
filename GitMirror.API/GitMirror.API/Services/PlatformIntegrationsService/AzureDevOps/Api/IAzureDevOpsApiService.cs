using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api;

public interface IAzureDevOpsApiService
{
    Task<Repository> CreateRepository(string baseUrl, string username, string password, Repository repository);
    Task<List<Repository>> GetRepositories(string baseUrl, string username, string password);
}
