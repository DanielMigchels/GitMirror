using GitMirror.Services.GitPlatforms.Bitbucket.Api;
using GitMirror.Services.GitPlatforms.Models;

namespace GitMirror.Services.GitPlatforms.Bitbucket;

public class BitbucketService(IBitbucketApiService bitbucketApiService) : IGitPlatformService
{
    public GitPlatformType GitPlatformType => GitPlatformType.Bitbucket;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public async Task<List<GitRepository>> GetRepositories()
    {
        return await bitbucketApiService.GetRepositories(BaseUrl, Username, Password);
    }

    public async Task<GitRepository> CreateRepository(GitRepository repository)
    {
        return await bitbucketApiService.CreateRepository(BaseUrl, Username, Password, repository);
    }
}
