using GitMirror.API.Data;
using GitMirror.API.Data.Models;
using GitMirror.API.Services.GitMirrorService;
using GitMirror.API.Services.PlatformIntegrationsService;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.PlatformMirrorService;

public class PlatformMirrorService(ILogger<PlatformMirrorService> logger, DatabaseContext db, IPlatformIntegrationServiceFactory gitPlatformServiceFactory, IGitMirrorService gitService) : IPlatformMirrorService
{
    public async Task Execute()
    {
        var mirrors = await db.Mirrors.AsNoTracking()
            .Where(x => x.SourcePlatform != null && x.TargetPlatform != null)
            .Select(x => new Mirror()
            {
                Id = x.Id,
                SourcePlatform = x.SourcePlatform,
                TargetPlatform = x.TargetPlatform
            }).ToListAsync();

        logger.LogInformation("Found {count} mirrors to process.", mirrors.Count);

        foreach (var mirror in mirrors)
        {
            if (mirror.SourcePlatform is null || mirror.TargetPlatform is null)
            {
                logger.LogWarning("Skipping mirror {MirrorId} due to missing platform information.", mirror.Id);
                continue;
            }

            await MirrorPlatform(mirror);
        }
    }

    public async Task MirrorPlatform(Mirror mirror)
    {
        logger.LogInformation("Repository mirror execution started for platforms {sourcePlatform} and {targetPlatform}.", mirror.SourcePlatform?.BaseUrl, mirror.TargetPlatform?.BaseUrl);

        if (mirror.SourcePlatform is null || mirror.TargetPlatform is null)
        {
            logger.LogWarning("Skipping mirror {MirrorId} due to missing platform information.", mirror.Id);
            return;
        }

        var sourcePlatformIntegration = gitPlatformServiceFactory.Create(mirror.SourcePlatform);
        var targetPlatformIntegration = gitPlatformServiceFactory.Create(mirror.TargetPlatform);

        var sourceRepositories = await sourcePlatformIntegration.GetRepositories();
        logger.LogInformation("Fetched {SourceCount} repositories from source.", sourceRepositories.Count);

        var targetRepositories = await targetPlatformIntegration.GetRepositories();
        logger.LogInformation("Fetched {TargetCount} repositories from target.", targetRepositories.Count);

        var originalAutoDetect = db.ChangeTracker.AutoDetectChangesEnabled;
        db.ChangeTracker.AutoDetectChangesEnabled = false;

        var histories = new List<Data.Models.History>();

        try
        {
            foreach (var sourceRepository in sourceRepositories)
            {
                logger.LogInformation("Processing repository {RepositoryName}.", sourceRepository.Name);

                var targetRepository = targetRepositories.FirstOrDefault(r => r.Name.Equals(sourceRepository.Name, StringComparison.OrdinalIgnoreCase)) 
                    ?? await targetPlatformIntegration.CreateRepository(sourceRepository);

                var history = new Data.Models.History
                {
                    Id = Guid.NewGuid(),
                    MirrorId = mirror.Id,
                    CreatedOnUtc = DateTimeOffset.UtcNow,
                    State = Data.Enums.HistoryState.InProgress
                };

                histories.Add(history);
                db.Histories.Add(history);

                try
                {
                    await gitService.MirrorAsync(sourceRepository.CloneUrl, mirror.SourcePlatform.Username, mirror.SourcePlatform.Password, targetRepository.CloneUrl, mirror.TargetPlatform.Username, mirror.TargetPlatform.Password);
                    history.State = Data.Enums.HistoryState.Successful;
                }
                catch
                {
                    history.State = Data.Enums.HistoryState.Failed;
                }

                logger.LogInformation("Completed mirroring {RepositoryName}.", sourceRepository.Name);

                await Task.Delay(1500);
            }

            await db.SaveChangesAsync();
        }
        finally
        {
            db.ChangeTracker.AutoDetectChangesEnabled = originalAutoDetect;
        }

        logger.LogInformation("Repository mirror execution completed.");
    }
}
