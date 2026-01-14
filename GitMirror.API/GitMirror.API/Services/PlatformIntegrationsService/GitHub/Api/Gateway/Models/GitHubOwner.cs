using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api.Gateway.Models
{
    public class GitHubOwner
    {
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
    }
}
