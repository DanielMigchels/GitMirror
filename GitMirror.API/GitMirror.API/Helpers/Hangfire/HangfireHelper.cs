using Hangfire;
using Hangfire.Storage;

namespace GitMirror.API.Helpers.Hangfire;

public class HangfireHelper
{
    public static bool RecurringJobExists(string id)
    {
        var recurringJobs = new List<RecurringJobDto>();
        using (var connection = JobStorage.Current.GetConnection())
        {
            recurringJobs.AddRange(connection.GetRecurringJobs());
        }

        return recurringJobs.Any(x => x.Id == id);
    }
}
