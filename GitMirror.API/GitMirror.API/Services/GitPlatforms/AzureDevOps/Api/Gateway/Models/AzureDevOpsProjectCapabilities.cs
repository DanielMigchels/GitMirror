using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.AzureDevOps.Api.Gateway.Models;

public class AzureDevOpsProjectCapabilities
{
    [JsonPropertyName("versionControl")]
    public AzureDevOpsVersionControlCapability VersionControl { get; set; } = new();

    [JsonPropertyName("processTemplate")]
    public AzureDevOpsProcessTemplateCapability ProcessTemplate { get; set; } = new();
}
