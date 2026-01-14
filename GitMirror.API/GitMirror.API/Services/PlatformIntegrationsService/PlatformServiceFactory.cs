using GitMirror.API.Data.Models;

namespace GitMirror.API.Services.PlatformIntegrationsService;

public class PlatformServiceFactory(IEnumerable<IPlatformService> services) : IPlatformServiceFactory
{
    public IPlatformService Create(Platform platform)
    {
        var service = services.Single(s => s.GitPlatformType == platform.Type);
        service.Username = platform.Username;
        service.Password = platform.Password;
        service.BaseUrl = platform.BaseUrl;
        return service;
    }
}
