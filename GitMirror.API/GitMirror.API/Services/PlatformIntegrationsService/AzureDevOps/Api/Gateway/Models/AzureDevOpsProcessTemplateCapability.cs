using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway.Models;

public class AzureDevOpsProcessTemplateCapability
{
    [JsonPropertyName("templateTypeId")]
    public string TemplateTypeId { get; set; } = string.Empty;
}