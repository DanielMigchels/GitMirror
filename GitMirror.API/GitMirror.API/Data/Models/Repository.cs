using System.ComponentModel.DataAnnotations;

namespace GitMirror.API.Data.Models;

public class Repository
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(1024)]
    public string SourceCloneUrl { get; set; } = string.Empty;
    [MaxLength(256)]
    public string SourceUsername { get; set; } = string.Empty;
    [MaxLength(256)]
    public string SourcePassword { get; set; } = string.Empty;
    [MaxLength(1024)]
    public string TargetCloneUrl { get; set; } = string.Empty;
    [MaxLength(256)]
    public string TargetUsername { get; set; } = string.Empty;
    [MaxLength(256)]
    public string TargetPassword { get; set; } = string.Empty;
}
