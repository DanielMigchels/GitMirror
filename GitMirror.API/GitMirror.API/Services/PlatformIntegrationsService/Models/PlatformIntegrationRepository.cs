namespace GitMirror.API.Services.PlatformIntegrationsService.Models;

public class PlatformIntegrationRepository
{
    public string Name { get; set; } = string.Empty;
    public string CloneUrl { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
}
