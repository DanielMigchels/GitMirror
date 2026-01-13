namespace GitMirror.Services.GitPlatforms;

public interface IGitPlatformServiceFactory
{
    IGitPlatformService Create(GitPlatform platform);
}
