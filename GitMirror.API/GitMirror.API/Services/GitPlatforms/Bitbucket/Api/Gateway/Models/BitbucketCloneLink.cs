using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.Bitbucket.Api.Gateway.Models
{
    public class BitbucketCloneLink
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("href")]
        public string Href { get; set; } = string.Empty;
    }
}
