using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.GitLab.Api.Gateway.Models
{
    public class GitLabNamespace
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
