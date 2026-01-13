using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.AzureDevOps.Api.Gateway.Models;

public class AzureDevOpsRepositories
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("value")]
    public List<AzureDevOpsRepository> Value { get; set; } = [];
}
