using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService;

public interface IPlatformService
{
    public PlatformType GitPlatformType { get; }
    string Username { get; set; }
    string Password { get; set; }
    string BaseUrl { get; set; }

    public Task<List<Repository>> GetRepositories();
    public Task<Repository> CreateRepository(Repository repository);
}
