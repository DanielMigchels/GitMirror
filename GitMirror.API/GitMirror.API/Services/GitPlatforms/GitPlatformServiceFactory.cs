namespace GitMirror.Services.GitPlatforms;

public class GitPlatformServiceFactory(IEnumerable<IGitPlatformService> services) : IGitPlatformServiceFactory
{
    public IGitPlatformService Create(GitPlatform platform)
    {
        var service = services.Single(s => s.GitPlatformType == platform.Type);
        service.Username = platform.Username;
        service.Password = platform.Password;
        service.BaseUrl = platform.BaseUrl;
        return service;
    }
}
