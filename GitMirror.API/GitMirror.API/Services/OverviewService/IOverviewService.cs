using GitMirror.API.Services.OverviewService.Models;

namespace GitMirror.API.Services.OverviewService;

public interface IOverviewService
{
    public Task<OverviewResponseModel> Get();
}
