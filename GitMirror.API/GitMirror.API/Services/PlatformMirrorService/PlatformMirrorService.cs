using GitMirror.API.Data.Models;
using GitMirror.API.Options;
using GitMirror.API.Services.GitMirrorService;
using GitMirror.API.Services.PlatformIntegrationsService;
using Microsoft.Extensions.Options;

namespace GitMirror.API.Services.PlatformMirrorService;

public class PlatformMirrorService(ILogger<PlatformMirrorService> logger, IOptionsMonitor<PlatformSettings> options, IPlatformServiceFactory gitPlatformServiceFactory, IGitMirrorService gitService) : IPlatformMirrorService
{
    private PlatformSettings AzureDevOps => options.Get("AzureDevOps");
    private PlatformSettings GitLab => options.Get("GitLab");

    public async Task Execute()
    {
        logger.LogInformation("Repository mirror execution started.");

        var sourcePlatform = gitPlatformServiceFactory.Create(new Platform
        {
            Type = PlatformType.AzureDevOps,
            BaseUrl = AzureDevOps.BaseUrl,
            Username = AzureDevOps.Username,
            Password = AzureDevOps.Password
        });

        var targetPlatform = gitPlatformServiceFactory.Create(new Platform
        {
            Type = PlatformType.GitLab,
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
