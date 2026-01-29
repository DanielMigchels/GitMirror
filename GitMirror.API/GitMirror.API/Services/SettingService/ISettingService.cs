using GitMirror.API.Services.SettingService.Models;

namespace GitMirror.API.Services.SettingService;

public interface ISettingService
{
    public Task<SettingModel> Get();
    public Task<bool> Update(SettingModel request);
    public Task<bool> TriggerJob(string jobName);
}
