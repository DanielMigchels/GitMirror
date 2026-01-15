namespace GitMirror.API.Services.MirrorService.Models;

public class MirrorResponseModel
{
    public Guid Id { get; set; }
    public Guid SourcePlatformId { get; set; }
    public Guid TargetPlatformId { get; set; }
}
