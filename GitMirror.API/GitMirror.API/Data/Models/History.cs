using GitMirror.API.Data.Enums;

namespace GitMirror.API.Data.Models;

public class History
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public HistoryState State { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; } = DateTimeOffset.UtcNow;

    public Guid? MirrorId { get; set; }
    public Mirror? Mirror { get; set; }

    public Guid? RepositoryId { get; set; }
    public Repository? Repository { get; set; }
}
