using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.AzureDevOps.Api.Gateway.Models;

public class AzureDevOpsVersionControlCapability
{
    [JsonPropertyName("sourceControlType")]
    public string SourceControlType { get; set; } = "Git";
}
