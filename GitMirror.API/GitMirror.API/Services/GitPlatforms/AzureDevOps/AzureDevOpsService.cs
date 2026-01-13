using GitMirror.Services.GitPlatforms.AzureDevOps.Api;
using GitMirror.Services.GitPlatforms.Models;

namespace GitMirror.Services.GitPlatforms.AzureDevOps;

public class AzureDevOpsService(IAzureDevOpsApiService azureDevOpsApiService) : IGitPlatformService
{
    public GitPlatformType GitPlatformType => GitPlatformType.AzureDevOps;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<GitRepository>> GetRepositories()
    {
        return await azureDevOpsApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<GitRepository> CreateRepository(GitRepository repository)
    {
        return await azureDevOpsApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
