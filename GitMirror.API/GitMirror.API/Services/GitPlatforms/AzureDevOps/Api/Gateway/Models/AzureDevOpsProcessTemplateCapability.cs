using System.Text.Json.Serialization;

namespace GitMirror.Services.GitPlatforms.AzureDevOps.Api.Gateway.Models;

public class AzureDevOpsProcessTemplateCapability
{
    [JsonPropertyName("templateTypeId")]
    public string TemplateTypeId { get; set; } = string.Empty;
}