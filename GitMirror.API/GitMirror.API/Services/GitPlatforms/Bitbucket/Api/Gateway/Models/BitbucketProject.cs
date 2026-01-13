using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.Bitbucket.Api.Gateway.Models
{
    public class BitbucketProject
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
