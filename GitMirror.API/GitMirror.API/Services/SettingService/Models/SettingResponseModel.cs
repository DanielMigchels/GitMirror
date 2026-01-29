namespace GitMirror.API.Services.SettingService.Models;

public class SettingResponseModel
{
    public string PlatformMirrorCron { get; set; } = string.Empty;
    public string RepositoryMirrorCron { get; set; } = string.Empty;
}
