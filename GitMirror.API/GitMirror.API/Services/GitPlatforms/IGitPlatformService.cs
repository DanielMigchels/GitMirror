using GitMirror.Services.GitPlatforms.Models;

namespace GitMirror.Services.GitPlatforms;

public interface IGitPlatformService
{
    public GitPlatformType GitPlatformType { get; }
    string Username { get; set; }
    string Password { get; set; }
    string BaseUrl { get; set; }

    public Task<List<GitRepository>> GetRepositories();
    public Task<GitRepository> CreateRepository(GitRepository repository);
}
