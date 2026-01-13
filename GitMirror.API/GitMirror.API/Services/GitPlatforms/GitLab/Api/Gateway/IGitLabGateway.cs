namespace GitMirror.Services.GitPlatforms.GitLab.Api.Gateway;

public interface IGitLabGateway
{
    Task<T> Get<T>(string baseUrl, string username, string password, string relativeUrl);
    Task<T> Post<T>(string baseUrl, string username, string password, string relativeUrl, object payload);
}
