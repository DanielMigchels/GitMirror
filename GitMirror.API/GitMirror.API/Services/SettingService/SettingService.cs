using GitMirror.API.Services.PlatformMirrorService;
using GitMirror.API.Services.RepositoryMirrorService;
using GitMirror.API.Services.SettingService.Models;
using Hangfire;
using Hangfire.Storage;

namespace GitMirror.API.Services.SettingService;

public class SettingService(ILogger<SettingService> logger) : ISettingService
{
    public Task<SettingModel> Get()
    {
        var recurringJobs = new List<RecurringJobDto>();
        using (var connection = JobStorage.Current.GetConnection())
        {
            recurringJobs.AddRange(connection.GetRecurringJobs());
        }

        return Task.FromResult(new SettingModel
        {
            PlatformMirrorCron = recurringJobs.Where(x => x.Id == "Execute Platform Mirror").Select(x => x.Cron).FirstOrDefault() ?? string.Empty,
            RepositoryMirrorCron = recurringJobs.Where(x => x.Id == "Execute Repository Mirror").Select(x => x.Cron).FirstOrDefault() ?? string.Empty
        });
    }

    public Task<bool> Update(SettingModel request)
    {
        try
        {
            RecurringJob.AddOrUpdate<IPlatformMirrorService>("Execute Platform Mirror", x => x.Execute(), request.PlatformMirrorCron, new RecurringJobOptions());
            RecurringJob.AddOrUpdate<IRepositoryMirrorService>("Execute Repository Mirror", x => x.Execute(), request.RepositoryMirrorCron, new RecurringJobOptions());
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured when updating settings in hangfire.");
            return Task.FromResult(false);
        }        
    }

    public Task<bool> TriggerJob(string jobName)
    {
        try
        {
            RecurringJob.TriggerJob(jobName);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"An error occurred when triggering job: {jobName}");
            return Task.FromResult(false);
        }
    }
}
