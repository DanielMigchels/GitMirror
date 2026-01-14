using GitMirror.API.Services.PlatformIntegrationsService;

namespace GitMirror.API.Services.PlatformService.Models;

public class PlatformRequestModel
{
    public PlatformType Type { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}
