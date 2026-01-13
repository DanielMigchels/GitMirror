using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GitMirror.Services.GitPlatforms.GitHub.Api.Gateway;

public class GitHubGateway(HttpClient httpClient) : IGitHubGateway
{
    private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public async Task<T> Get<T>(string baseUrl, string username, string password, string relativeUrl)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl.TrimEnd('/')}{relativeUrl}");
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", password);
        request.Headers.Add("User-Agent", "GitMirror");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
    
        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, options) ?? throw new InvalidOperationException("Deserialization returned null.");
    }

    public async Task<T> Post<T>(string baseUrl, string username, string password, string relativeUrl, object payload)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl.TrimEnd('/')}{relativeUrl}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", password);
        request.Headers.Add("User-Agent", "GitMirror");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, options) ?? throw new InvalidOperationException("Deserialization returned null.");
    }

    public async Task<T> Patch<T>(string baseUrl, string username, string password, string relativeUrl, object payload)
    {
        using var request = new HttpRequestMessage(HttpMethod.Patch, $"{baseUrl.TrimEnd('/')}{relativeUrl}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", password);
        request.Headers.Add("User-Agent", "GitMirror");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, options) ?? throw new InvalidOperationException("Deserialization returned null.");
    }
}
