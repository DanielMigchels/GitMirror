using GitMirror.API.Data;
using GitMirror.API.Services.OverviewService.Models;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.OverviewService;

public class OverviewService(DatabaseContext db) : IOverviewService
{
    public async Task<OverviewResponseModel> Get()
    {
        var response = new OverviewResponseModel
        {
            RepositoryCount = await db.Repositories.CountAsync(),
            MirrorCount = await db.Mirrors.CountAsync(),
            PlatformCount = await db.Platforms.CountAsync(),
            HistoryCount = await db.Histories.CountAsync()
        };

        return response;
    }
}
