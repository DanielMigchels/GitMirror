using GitMirror.API.Services.SettingService.Models;

namespace GitMirror.API.Services.SettingService;

public class SettingService : ISettingService
{
    public Task<SettingResponseModel> Get()
    {
        return Task.FromResult(new SettingResponseModel());
    }
}
