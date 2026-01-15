using GitMirror.API.Services.PlatformIntegrationsService;

namespace GitMirror.API.Services.PlatformService.Models;

public class PlatformResponseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public PlatformIntegrationType Type { get; set; }
    public string Username { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}
