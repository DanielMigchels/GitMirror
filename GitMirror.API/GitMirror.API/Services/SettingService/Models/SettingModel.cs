namespace GitMirror.API.Services.SettingService.Models;

public class SettingModel
{
    public string PlatformMirrorCron { get; set; } = string.Empty;
    public string RepositoryMirrorCron { get; set; } = string.Empty;
}
