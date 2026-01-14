using System.Text.Json.Serialization;

namespace GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway.Models
{
    public class AzureDevOpsRepository
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("project")]
        public AzureDevOpsProject Project { get; set; } = new();

        [JsonPropertyName("defaultBranch")]
        public string DefaultBranch { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("remoteUrl")]
        public string RemoteUrl { get; set; } = string.Empty;

        [JsonPropertyName("sshUrl")]
        public string SshUrl { get; set; } = string.Empty;

        [JsonPropertyName("webUrl")]
        public string WebUrl { get; set; } = string.Empty;

        [JsonPropertyName("isDisabled")]
        public bool IsDisabled { get; set; }

        [JsonPropertyName("isInMaintenance")]
        public bool IsInMaintenance { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
