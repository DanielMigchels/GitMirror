using GitMirror.API.Services.PlatformIntegrationsService;
using System.ComponentModel.DataAnnotations;

namespace GitMirror.API.Data.Models;

public class Platform
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public PlatformType Type { get; set; }

    [MaxLength(256)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(256)]
    public string Password { get; set; } = string.Empty;

    [MaxLength(1024)]
    public string BaseUrl { get; set; } = string.Empty;
}
