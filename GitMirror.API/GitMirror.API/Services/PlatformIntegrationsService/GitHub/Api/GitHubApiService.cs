using GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api.Gateway.Models;
using GitMirror.API.Services.PlatformIntegrationsService.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api;

public class GitHubApiService(IGitHubGateway gitHubGateway) : IGitHubApiService
{
    public async Task<List<Repository>> GetRepositories(string baseUrl, string username, string password)
    {
        var perPage = 100;
        var page = 1;
        var allRepositories = new List<Repository>();

        while (true)
        {
            var repos = await gitHubGateway.Get<List<GitHubRepository>>(baseUrl, username, password, $"/user/repos?per_page={perPage}&page={page}&affiliation=owner");

            if (repos == null || repos.Count == 0)
            {
                break;
            }

            allRepositories.AddRange(repos.Select(r => new Repository
            {
                Name = r.Name,
                CloneUrl = r.CloneUrl,
                Project = r.Owner?.Login ?? string.Empty
            }));

            if (repos.Count < perPage)
            {
                break;
            }

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

        var payload = new
        {
            name = repository.Name,
            @private = true,
            auto_init = false
        };

        var createdRepository = await gitHubGateway.Post<GitHubRepository>(baseUrl, username, password, "/user/repos", payload);

        return new Repository
        {
            Name = createdRepository.Name,
            CloneUrl = createdRepository.CloneUrl,
            Project = createdRepository.Owner?.Login ?? string.Empty,
        };
    }
}
