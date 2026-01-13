namespace GitMirror.Services.GitPlatforms.GitHub.Api.Gateway;

public interface IGitHubGateway
{
    Task<T> Get<T>(string baseUrl, string username, string password, string relativeUrl);
    Task<T> Post<T>(string baseUrl, string username, string password, string relativeUrl, object payload);
    Task<T> Patch<T>(string baseUrl, string username, string password, string relativeUrl, object payload);
}
