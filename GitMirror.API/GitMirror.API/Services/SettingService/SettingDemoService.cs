using GitMirror.API.Services.SettingService.Models;

namespace GitMirror.API.Services.SettingService
{
    public class SettingDemoService : ISettingService
    {
        private readonly SettingModel _demoSettings = new()
        {
            PlatformMirrorCron = "0 0 * * *",  // Daily at midnight
            RepositoryMirrorCron = "0 */6 * * *"  // Every 6 hours
        };

        public Task<SettingModel> Get()
        {
            return Task.FromResult(_demoSettings);
        }

        public Task<bool> TriggerJob(string jobName)
        {
            return Task.FromResult(true);
        }

        public Task<bool> Update(SettingModel request)
        {
            return Task.FromResult(true);
        }
    }
}
