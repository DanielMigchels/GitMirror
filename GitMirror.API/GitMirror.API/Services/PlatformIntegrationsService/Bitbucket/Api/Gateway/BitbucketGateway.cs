using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api.Gateway;

public class BitbucketGateway(HttpClient httpClient) : IBitbucketGateway
{
    private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public async Task<T> Get<T>(string baseUrl, string username, string password, string relativeUrl)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl.TrimEnd('/')}{relativeUrl}");
        
        var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
    
        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, options) ?? throw new InvalidOperationException("Deserialization returned null.");
    }

    public async Task<T> Post<T>(string baseUrl, string username, string password, string relativeUrl, object payload)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl.TrimEnd('/')}{relativeUrl}");
        
        var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, options) ?? throw new InvalidOperationException("Deserialization returned null.");
    }
}
