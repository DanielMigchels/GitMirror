using GitMirror.API.Data;
using GitMirror.API.Data.Enums;
using GitMirror.API.Services.PlatformIntegrationsService;
using Microsoft.EntityFrameworkCore;

namespace GitMirror.API.Services.SeedService;

public class SeedService(DatabaseContext db) : ISeedService
{
    private const int MinAmountOfHistories = 25;
    private const int MaxAmountOfHistories = 100;

    private static readonly string[] ProjectNames = 
    {
        "customer-portal", "backend-api", "mobile-app", "analytics-service", 
        "payment-gateway", "notification-service", "user-management", "inventory-system",
        "reporting-dashboard", "authentication-service", "data-pipeline", "web-client",
        "microservice-core", "logging-infrastructure", "monitoring-tools", "ci-cd-scripts",
        "documentation-site", "admin-panel", "email-service", "file-storage-api"
    };

    private static readonly string[] Organizations = 
    {
        "acme-corp", "tech-solutions", "digital-ventures", "cloud-systems",
        "enterprise-apps", "software-inc", "innovative-tech", "data-dynamics"
    };

    public async Task SeedFakeHistory()
    {
        Random random = new Random();

        var mirror = db.Mirrors.Include(x => x.TargetPlatform).Include(x => x.SourcePlatform).FirstOrDefault();

        if (mirror == null)
        {
            mirror = new Data.Models.Mirror
            {
                SourcePlatform = new Data.Models.Platform
                {
                    Type = PlatformIntegrationType.GitHub,
                    Username = "demo-user",
                    Password = "***REDACTED***",
                    BaseUrl = "https://github.com"
                },
                TargetPlatform = new Data.Models.Platform
                {
                    Type = PlatformIntegrationType.GitLab,
                    Username = "gitlab-mirror",
                    Password = "***REDACTED***",
                    BaseUrl = "https://gitlab.com"
                }
            };
            db.Mirrors.Add(mirror);
        }

        int totalHistories = random.Next(MinAmountOfHistories, MaxAmountOfHistories);
        var startDate = DateTimeOffset.UtcNow.AddDays(-1);

        for (int i = 0; i < totalHistories; i++)
        {
            var organization = Organizations[random.Next(Organizations.Length)];
            var projectName = ProjectNames[random.Next(ProjectNames.Length)];
            
            var sourceUrl = GetPlatformUrl(mirror.SourcePlatform!.Type, organization, projectName);
            var targetUrl = GetPlatformUrl(mirror.TargetPlatform!.Type, organization, projectName);

            var state = HistoryState.Successful;

            var history = new Data.Models.History
            {
                State = state,
                Mirror = mirror,
                SourceUrl = sourceUrl,
                TargetUrl = targetUrl,
                CreatedOnUtc = startDate.AddMinutes(random.Next(0, 24 * 60))
            };

            db.Histories.Add(history);
        }

        await db.SaveChangesAsync();
    }

    private static string GetPlatformUrl(PlatformIntegrationType type, string organization, string projectName)
    {
        return type switch
        {
            PlatformIntegrationType.GitHub => $"https://github.com/{organization}/{projectName}.git",
            PlatformIntegrationType.GitLab => $"https://gitlab.com/{organization}/{projectName}.git",
            PlatformIntegrationType.AzureDevOps => $"https://dev.azure.com/{organization}/_git/{projectName}",
            PlatformIntegrationType.Bitbucket => $"https://bitbucket.org/{organization}/{projectName}.git",
            _ => $"https://example.com/{organization}/{projectName}.git"
        };
    }
}
