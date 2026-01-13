namespace GitMirror.Services.GitPlatforms.Bitbucket.Api.Gateway;

public interface IBitbucketGateway
{
    Task<T> Get<T>(string baseUrl, string username, string password, string relativeUrl);
    Task<T> Post<T>(string baseUrl, string username, string password, string relativeUrl, object payload);
}
