using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.GitLab.Api.Gateway.Models
{
    public class GitLabProject
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("http_url_to_repo")]
        public string HttpUrlToRepo { get; set; } = string.Empty;

        [JsonPropertyName("namespace")]
        public GitLabNamespace Namespace { get; set; } = new GitLabNamespace();

        [JsonPropertyName("namespace_id")]
        public int? NamespaceId { get; set; }

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; } = string.Empty;
    }
}
