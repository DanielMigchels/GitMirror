using GitMirror.API.Data.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService;

public interface IPlatformServiceFactory
{
    IPlatformService Create(Platform platform);
}
