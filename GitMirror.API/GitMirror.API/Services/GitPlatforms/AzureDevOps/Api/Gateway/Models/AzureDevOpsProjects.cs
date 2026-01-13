using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.AzureDevOps.Api.Gateway.Models;

public class AzureDevOpsProjects
{
    [JsonPropertyName("value")]
    public List<AzureDevOpsProject> Value { get; set; } = [];
}
