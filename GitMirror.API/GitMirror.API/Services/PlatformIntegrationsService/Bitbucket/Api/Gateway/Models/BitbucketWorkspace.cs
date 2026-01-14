using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api.Gateway.Models
{
    public class BitbucketWorkspace
    {
        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
