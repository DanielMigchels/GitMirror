using GitMirror.Options;
using GitMirror.Services.Git;
using GitMirror.Services.GitMirror;
using GitMirror.Services.GitPlatforms;
using Microsoft.Extensions.Options;

namespace GitMirror.Services.RepositoryMirror;

public class RepositoryMirrorService(ILogger<RepositoryMirrorService> logger, IOptionsMonitor<GitPlatformSettings> options, IGitPlatformServiceFactory gitPlatformServiceFactory, IGitService gitService) : IRepositoryMirrorService
{
    private GitPlatformSettings AzureDevOps => options.Get("AzureDevOps");
    private GitPlatformSettings GitLab => options.Get("GitLab");

    public async Task Execute()
    {
        logger.LogInformation("Repository mirror execution started.");

        var sourcePlatform = gitPlatformServiceFactory.Create(new GitPlatform
        {
            Type = GitPlatformType.AzureDevOps,
            BaseUrl = AzureDevOps.BaseUrl,
            Username = AzureDevOps.Username,
            Password = AzureDevOps.Password
        });

        var targetPlatform = gitPlatformServiceFactory.Create(new GitPlatform
        {
            Type = GitPlatformType.GitLab,
            BaseUrl = GitLab.BaseUrl,
            Username = GitLab.Username,
            Password = GitLab.Password
        });

        var sourceRepositories = await sourcePlatform.GetRepositories();
        logger.LogInformation("Fetched {SourceCount} repositories from source.", sourceRepositories.Count);

        var targetRepositories = await targetPlatform.GetRepositories();
        logger.LogInformation("Fetched {TargetCount} repositories from target.", targetRepositories.Count);

        foreach (var sourceRepository in sourceRepositories)
        {
            logger.LogInformation("Processing repository {RepositoryName}.", sourceRepository.Name);

            var targetRepository = targetRepositories.FirstOrDefault(r => r.Name.Equals(sourceRepository.Name, StringComparison.OrdinalIgnoreCase)) 
                ?? await targetPlatform.CreateRepository(sourceRepository);

            try
            {
                await gitService.MirrorAsync(sourceRepository.CloneUrl, sourcePlatform.Username, sourcePlatform.Password, targetRepository.CloneUrl, targetPlatform.Username, targetPlatform.Password);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not mirror repository {repository}", sourceRepository.Name);
            }
            
            logger.LogInformation("Completed mirroring {RepositoryName}.", sourceRepository.Name);

            await Task.Delay(1500);
        }

        logger.LogInformation("Repository mirror execution completed.");
    }
}
