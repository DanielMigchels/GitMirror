using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api.Gateway.Models
{
    public class GitLabNamespace
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
