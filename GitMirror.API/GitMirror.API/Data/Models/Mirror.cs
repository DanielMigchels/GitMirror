namespace GitMirror.API.Data.Models;

public class Mirror
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SourcePlatformId { get; set; }
    public Platform? SourcePlatform { get; set; }

    public Guid TargetPlatformId { get; set; }
    public Platform? TargetPlatform { get; set; }
}
