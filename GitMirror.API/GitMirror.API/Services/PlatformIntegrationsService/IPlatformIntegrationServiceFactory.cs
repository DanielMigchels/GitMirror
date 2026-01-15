using GitMirror.API.Data.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService;

public interface IPlatformIntegrationServiceFactory
{
    IPlatformIntegrationService Create(Platform platform);
}
