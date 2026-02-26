using GitMirror.API.Services.OverviewService.Models;

namespace GitMirror.API.Services.OverviewService
{
    public class OverviewDemoService : IOverviewService
    {
        private readonly Random _random = new(42);

        public Task<OverviewResponseModel> Get()
        {
            var repositoryCount = 1;
            var mirrorCount = 1;
            var platformCount = 0;
            var queuedCount = 1;
            var inProgressCount = 1;
            var successfulCount = 123;
            var failedCount = 2;

            var dailyActivity = GenerateDailyActivity();
            var recentHistory = GenerateRecentHistory();

            return Task.FromResult(new OverviewResponseModel
            {
                RepositoryCount = repositoryCount,
                MirrorCount = mirrorCount,
                PlatformCount = platformCount,
                HistoryCount = queuedCount + inProgressCount + successfulCount + failedCount,
                QueuedCount = queuedCount,
                InProgressCount = inProgressCount,
                SuccessfulCount = successfulCount,
                FailedCount = failedCount,
                DailyActivity = dailyActivity,
                RecentHistory = recentHistory,
                IsDemoMode = true,
            });
        }

        private List<DailyActivityModel> GenerateDailyActivity()
        {
            var dailyActivity = new List<DailyActivityModel>();
            var today = DateTimeOffset.UtcNow.Date;

            for (int i = 29; i >= 0; i--)
            {
                var date = today.AddDays(-i);
                var successful = _random.Next(23, 25);
                var failed = _random.Next(0, 2);
                
                dailyActivity.Add(new DailyActivityModel
                {
                    Date = date.ToString("yyyy-MM-dd"),
                    Successful = successful,
                    Failed = failed,
                    Total = successful + failed
                });
            }

            return dailyActivity;
        }

        private List<RecentHistoryModel> GenerateRecentHistory()
        {
            var recentHistory = new List<RecentHistoryModel>();
            var states = new[] { "Successful" };
            var sourceRepos = new[] 
            { 
                "https://github.com/acme/api.git",
                "https://gitlab.com/corp/backend.git",
                "https://github.com/team/frontend.git",
                "https://dev.azure.com/org/project.git"
            };
            var targetRepos = new[]
            {
                "https://bitbucket.org/backup/api.git",
                "https://github.com/mirror/backend.git",
                "https://gitlab.com/archive/frontend.git",
                "https://bitbucket.org/archive/project.git"
            };

            var now = DateTimeOffset.UtcNow;
            for (int i = 0; i < 10; i++)
            {
                var sourceUrl = sourceRepos[_random.Next(sourceRepos.Length)];
                var targetUrl = targetRepos[_random.Next(targetRepos.Length)];
                var state = states[_random.Next(states.Length)];
                var minutesAgo = _random.Next(1, 240);

                recentHistory.Add(new RecentHistoryModel
                {
                    Id = Guid.NewGuid().ToString(),
                    State = state,
                    SourceUrl = sourceUrl,
                    TargetUrl = targetUrl,
                    CreatedOnUtc = now.AddMinutes(-minutesAgo)
                });
            }

            return recentHistory.OrderByDescending(h => h.CreatedOnUtc).ToList();
        }
    }
}
