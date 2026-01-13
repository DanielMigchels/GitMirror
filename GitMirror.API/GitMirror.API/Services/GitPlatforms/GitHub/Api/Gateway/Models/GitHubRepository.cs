using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.GitHub.Api.Gateway.Models
{
    public class GitHubRepository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("clone_url")]
        public string CloneUrl { get; set; } = string.Empty;

        [JsonPropertyName("owner")]
        public GitHubOwner Owner { get; set; } = new GitHubOwner();

        [JsonPropertyName("private")]
        public bool Private { get; set; } = true;

        [JsonPropertyName("org")]
        public string? Org { get; set; }
    }
}
