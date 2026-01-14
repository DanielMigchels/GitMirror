using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway.Models;
using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api;

public class AzureDevOpsApiService(IAzureDevOpsGateway azureDevOpsGateway) : IAzureDevOpsApiService
{
    public async Task<List<Repository>> GetRepositories(string baseUrl, string username, string password)
    {
        var repositories = await azureDevOpsGateway.Get<AzureDevOpsRepositories>(baseUrl, username, password, "/_apis/git/repositories?api-version=6.0");
        return [.. repositories.Value.Select(r => new Repository
        {
            Name = r.Name,
            CloneUrl = r.RemoteUrl,
            Project = r.Project?.Name ?? string.Empty
        })];
    }

    public async Task<Repository> CreateRepository(string baseUrl, string username, string password, Repository repository)
    {
        if (repository == null)
        {
            throw new Exception("Repository is null");
        }

        var projectId = await EnsureProjectExists(baseUrl, username, password, repository.Project);

        var payload = new AzureDevOpsRepository
        {
            Name = repository.Name,
            Project = projectId != null ? new AzureDevOpsProject { Id = projectId, Name = repository.Project } : new AzureDevOpsProject { Name = repository.Project }
        };

        var createdRepository = await azureDevOpsGateway.Post<AzureDevOpsRepository, AzureDevOpsRepository>(baseUrl, username, password, "/_apis/git/repositories?api-version=6.0", payload);

        return new Repository
        {
            Name = createdRepository.Name,
            CloneUrl = createdRepository.RemoteUrl,
            Project = createdRepository.Project?.Name ?? string.Empty
        };
    }

    private async Task<string?> EnsureProjectExists(string baseUrl, string username, string password, string projectName)
    {
        AzureDevOpsProjects projects = new() { Value = [] };
        try
        {
            projects = await azureDevOpsGateway.Get<AzureDevOpsProjects>(baseUrl, username, password, "/_apis/projects?api-version=6.0");
        }
        catch { }

        var existing = projects.Value.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));
        if (existing != null)
        {
            return existing.Id;
        }

        var payload = new AzureDevOpsProjectCreate
        {
            Name = projectName,
            Description = $"Project for {projectName}",
            Capabilities = new AzureDevOpsProjectCapabilities
            {
                VersionControl = new() { SourceControlType = "Git" },
                ProcessTemplate = new() { TemplateTypeId = "adcc2f6f-bf88-4b4f-9c29-1b4b62a7f2b4" }
            }
        };

        try
        {
            var created = await azureDevOpsGateway.Post<AzureDevOpsProject, AzureDevOpsProjectCreate>(baseUrl, username, password, "/_apis/projects?api-version=6.0", payload);
            return created?.Id;
        }
        catch { return null; }
    }
}
