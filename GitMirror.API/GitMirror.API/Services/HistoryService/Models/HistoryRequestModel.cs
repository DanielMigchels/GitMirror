using GitMirror.API.Data.Enums;

namespace GitMirror.API.Services.HistoryService.Models;

public class HistoryRequestModel
{
    public HistoryState State { get; set; }
    public Guid? MirrorId { get; set; }
    public Guid? RepositoryId { get; set; }
}
