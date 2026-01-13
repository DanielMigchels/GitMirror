using GitMirror.Services.GitPlatforms.Models;

namespace GitMirror.Services.GitPlatforms.AzureDevOps.Api;

public interface IAzureDevOpsApiService
{
    Task<GitRepository> CreateRepository(string baseUrl, string username, string password, GitRepository repository);
    Task<List<GitRepository>> GetRepositories(string baseUrl, string username, string password);
}
