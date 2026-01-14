using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api.Gateway.Models
{
    public class BitbucketPagedResponse<T>
    {
        [JsonPropertyName("values")]
        public List<T> Values { get; set; } = new List<T>();

        [JsonPropertyName("next")]
        public string? Next { get; set; }
    }
}
