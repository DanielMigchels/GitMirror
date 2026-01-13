using GitMirror.Services.GitPlatforms.Models;

namespace GitMirror.Services.GitPlatforms.GitHub.Api;

public interface IGitHubApiService
{
    Task<GitRepository> CreateRepository(string baseUrl, string username, string password, GitRepository repository);
    Task<List<GitRepository>> GetRepositories(string baseUrl, string username, string password);
}
