using GitMirror.Services.GitPlatforms.Bitbucket.Api.Gateway;
using GitMirror.Services.GitPlatforms.Bitbucket.Api.Gateway.Models;
using GitMirror.Services.GitPlatforms.Models;

namespace GitMirror.Services.GitPlatforms.Bitbucket.Api;

public class BitbucketApiService(IBitbucketGateway bitbucketGateway) : IBitbucketApiService
{
    public async Task<List<GitRepository>> GetRepositories(string baseUrl, string username, string password)
    {
        var allRepositories = new List<GitRepository>();
        string? nextUrl = $"/2.0/repositories/{username}?pagelen=100";

        while (!string.IsNullOrEmpty(nextUrl))
        {
            var response = await bitbucketGateway.Get<BitbucketPagedResponse<BitbucketRepository>>(baseUrl, username, password, nextUrl);

            if (response?.Values == null || response.Values.Count == 0)
            {
                break;
            }

            allRepositories.AddRange(response.Values.Select(r => new GitRepository
            {
                Name = r.Name,
                CloneUrl = r.Links?.Clone?.FirstOrDefault(c => c.Name == "https")?.Href ?? string.Empty,
                Project = r.Workspace?.Slug ?? string.Empty
            }));

            nextUrl = response.Next != null ? new Uri(response.Next).PathAndQuery : null;
        }

        return allRepositories;
    }

    public async Task<GitRepository> CreateRepository(string baseUrl, string username, string password, GitRepository repository)
    {
        if (repository == null)
        {
            throw new Exception("Repository is null");
        }

        var workspace = string.IsNullOrEmpty(repository.Project) ? username : repository.Project;
        
        string? projectKey = null;
        if (!string.IsNullOrEmpty(repository.Project))
        {
            projectKey = await EnsureProjectExists(baseUrl, username, password, workspace, repository.Project);
        }

        var slug = repository.Name.ToLower().Replace(" ", "-").Replace("_", "-");

        var payload = new
        {
            scm = "git",
            is_private = true,
            name = repository.Name,
            project = projectKey != null ? new { key = projectKey } : null
        };

        var createdRepository = await bitbucketGateway.Post<BitbucketRepository>(baseUrl, username, password, $"/2.0/repositories/{workspace}/{slug}", payload);

        return new GitRepository
        {
            Name = createdRepository.Name,
            CloneUrl = createdRepository.Links?.Clone?.FirstOrDefault(c => c.Name == "https")?.Href ?? string.Empty,
            Project = createdRepository.Workspace?.Slug ?? string.Empty,
        };
    }

    private async Task<string?> EnsureProjectExists(string baseUrl, string username, string password, string workspace, string projectName)
    {
        try
        {
            var response = await bitbucketGateway.Get<BitbucketPagedResponse<BitbucketProject>>(baseUrl, username, password, $"/2.0/workspaces/{workspace}/projects?pagelen=100");
            
            var existingProject = response?.Values?.FirstOrDefault(p => 
                p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase) ||
                p.Key.Equals(projectName, StringComparison.OrdinalIgnoreCase));
            
            if (existingProject != null)
            {
                return existingProject.Key;
            }

            var projectKey = projectName.ToUpper().Replace(" ", "").Replace("-", "").Substring(0, Math.Min(projectName.Length, 10));
            var payload = new
            {
                name = projectName,
                key = projectKey,
                is_private = true
            };

            var createdProject = await bitbucketGateway.Post<BitbucketProject>(baseUrl, username, password, $"/2.0/workspaces/{workspace}/projects", payload);
            return createdProject?.Key;
        }
        catch
        {
            return null;
        }
    }
}
