using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.GitLab.Api.Gateway.Models
{
    public class GitLabGroup
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}
