using GitMirror.API.Data;
using GitMirror.API.Services.GitMirrorService;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.RepositoryMirrorService;

public class RepositoryMirrorService(ILogger<RepositoryMirrorService> logger, DatabaseContext db, IGitMirrorService gitMirrorService) : IRepositoryMirrorService
{
    public async Task Execute()
    {
        logger.LogInformation("Starting repository mirroring process.");

        var repositories = await db.Repositories.AsNoTracking().Select(x => new
        {
            x.Id,
            x.SourceCloneUrl,
            x.SourceUsername,
            x.SourcePassword,
            x.TargetCloneUrl,
            x.TargetUsername,
            x.TargetPassword
        }).ToListAsync();

        foreach (var repository in repositories)
        {
            var history = new Data.Models.History
            {
                Id = Guid.NewGuid(),
                RepositoryId = repository.Id,
                CreatedOnUtc = DateTimeOffset.UtcNow,
                State = Data.Enums.HistoryState.InProgress,
                MirrorId = null,
                SourceUrl = repository.SourceCloneUrl,
                TargetUrl = repository.TargetCloneUrl,
            };

            db.Histories.Add(history);
            await db.SaveChangesAsync();

            try
            {
                await gitMirrorService.MirrorAsync(repository.SourceCloneUrl, repository.SourceUsername, repository.SourcePassword, repository.TargetCloneUrl, repository.TargetUsername, repository.TargetPassword);
                history.State = Data.Enums.HistoryState.Successful;
            }
            catch
            {
                history.State = Data.Enums.HistoryState.Failed;
            }


            await db.SaveChangesAsync();
        }

    }
}
