using GitMirror.API.Services.SettingService.Models;
using Hangfire;
using Hangfire.Storage;

namespace GitMirror.API.Services.SettingService;

public class SettingService : ISettingService
{
    public Task<SettingResponseModel> Get()
    {
        var recurringJobs = new List<RecurringJobDto>();
        using (var connection = JobStorage.Current.GetConnection())
        {
            recurringJobs.AddRange(connection.GetRecurringJobs());
        }

        return Task.FromResult(new SettingResponseModel
        {
            PlatformMirrorCron = recurringJobs.Where(x => x.Id == "Execute Platform Mirror").Select(x => x.Cron).FirstOrDefault() ?? string.Empty,
            RepositoryMirrorCron = recurringJobs.Where(x => x.Id == "Execute Repository Mirror").Select(x => x.Cron).FirstOrDefault() ?? string.Empty
        });
    }
}
