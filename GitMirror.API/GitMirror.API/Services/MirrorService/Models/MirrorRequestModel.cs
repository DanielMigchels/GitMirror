namespace GitMirror.API.Services.MirrorService.Models;

public class MirrorRequestModel
{
    public Guid SourcePlatformId { get; set; }
    public Guid TargetPlatformId { get; set; }
}
