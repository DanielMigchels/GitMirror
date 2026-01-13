using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.Bitbucket.Api.Gateway.Models
{
    public class BitbucketLinks
    {
        [JsonPropertyName("clone")]
        public List<BitbucketCloneLink>? Clone { get; set; }
    }
}
