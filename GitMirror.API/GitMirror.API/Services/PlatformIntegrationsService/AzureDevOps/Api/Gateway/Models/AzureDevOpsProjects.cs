using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway.Models;

public class AzureDevOpsProjects
{
    [JsonPropertyName("value")]
    public List<AzureDevOpsProject> Value { get; set; } = [];
}
