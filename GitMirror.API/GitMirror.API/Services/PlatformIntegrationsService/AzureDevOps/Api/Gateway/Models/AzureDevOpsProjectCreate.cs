using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway.Models;

public class AzureDevOpsProjectCreate
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("capabilities")]
    public AzureDevOpsProjectCapabilities Capabilities { get; set; } = new();
}
