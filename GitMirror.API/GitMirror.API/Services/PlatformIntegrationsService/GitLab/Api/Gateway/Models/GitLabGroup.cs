using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api.Gateway.Models
{
    public class GitLabGroup
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}
