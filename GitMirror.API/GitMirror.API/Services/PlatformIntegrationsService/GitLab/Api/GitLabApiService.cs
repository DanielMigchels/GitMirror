using GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api.Gateway.Models;
using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api;

public class GitLabApiService(IGitLabGateway gitLabGateway) : IGitLabApiService
{
    public async Task<List<Repository>> GetRepositories(string baseUrl, string username, string password)
    {
        var perPage = 20;
        var page = 1;
        var allRepositories = new List<Repository>();

        while (true)
        {
            var projects = await gitLabGateway.Get<List<GitLabProject>>(baseUrl, username, password, $"/api/v4/projects?owned=true&per_page={perPage}&page={page}");

            if (projects == null || projects.Count == 0)
            {
                break;
            }                

            allRepositories.AddRange(projects.Select(r => new Repository
            {
                Name = r.Name,
                CloneUrl = r.HttpUrlToRepo,
                Project = r.Namespace?.Name ?? string.Empty
            }));

            page++;
        }

        return allRepositories;
    }


    public async Task<Repository> CreateRepository(string baseUrl, string username, string password, Repository repository)
    {
        if (repository == null)
        {
            throw new Exception("Repository is null");
        }

        var namespaceId = await EnsureGroupExists(baseUrl, username, password, repository.Project);

        var payload = new GitLabProject()
        {
            Name = repository.Name,
            NamespaceId = namespaceId,
            Visibility = "private"
        };

        var createdRepository = await gitLabGateway.Post<GitLabProject>(baseUrl, username, password, "/api/v4/projects", payload);

        return new Repository
        {
            Name = createdRepository.Name,
            CloneUrl = createdRepository.HttpUrlToRepo,
            Project = createdRepository.Namespace?.Name ?? string.Empty,
        };
    }

    private async Task<int?> EnsureGroupExists(string baseUrl, string username, string password, string groupName)
    {
        List<GitLabGroup> groups = [];
        try
        {
            groups = await gitLabGateway.Get<List<GitLabGroup>>(baseUrl, username, password, $"/api/v4/groups?owned=true&per_page=1000");
        }
        catch { }

        var existingGroup = groups.FirstOrDefault(g => g.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
        if (existingGroup != null)
        {
            return existingGroup.Id;
        }

        var payload = new
        {
            name = groupName,
            path = groupName.Replace(" ", "-").ToLower(),
            visibility = "private"
        };

        try
        {
            var createdGroup = await gitLabGateway.Post<GitLabGroup>(baseUrl, username, password, "/api/v4/groups", payload);
            return createdGroup?.Id;
        }
        catch
        {
            return null;
        }
    }
}