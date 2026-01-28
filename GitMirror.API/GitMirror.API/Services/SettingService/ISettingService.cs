using GitMirror.API.Services.SettingService.Models;

namespace GitMirror.API.Services.SettingService;

public interface ISettingService
{
    public Task<SettingResponseModel> Get();
}
