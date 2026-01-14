namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway;

public interface IAzureDevOpsGateway
{
    public Task<T> Get<T>(string baseUrl, string username, string password, string action);
    public Task<T> Post<T, T2>(string baseUrl, string username, string password, string action, T2 query);
}
