using System.Net.Http.Headers;
using System.Text.Json;

namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway;

public class AzureDevOpsGateway(HttpClient client) : IAzureDevOpsGateway
{
    public async Task<T> Get<T>(string baseUrl, string username, string password, string action)
    {
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

        var url = $"{baseUrl.TrimEnd('/')}/{action.TrimStart('/')}";
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseString) ?? throw new InvalidOperationException("Deserialization returned null.");
    }

    public async Task<T> Post<T, T2>(string baseUrl, string username, string password, string action, T2 query)
    {
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

        var url = $"{baseUrl.TrimEnd('/')}/{action.TrimStart('/')}";
        var content = new StringContent(JsonSerializer.Serialize(query), System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseString) ?? throw new InvalidOperationException("Deserialization returned null.");
    }
}
