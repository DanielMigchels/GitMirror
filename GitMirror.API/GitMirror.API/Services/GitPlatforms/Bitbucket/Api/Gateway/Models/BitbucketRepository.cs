using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.Bitbucket.Api.Gateway.Models
{
    public class BitbucketRepository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("links")]
        public BitbucketLinks? Links { get; set; }

        [JsonPropertyName("workspace")]
        public BitbucketWorkspace? Workspace { get; set; }

        [JsonPropertyName("project")]
        public BitbucketProject? Project { get; set; }

        [JsonPropertyName("is_private")]
        public bool IsPrivate { get; set; } = true;
    }
}
