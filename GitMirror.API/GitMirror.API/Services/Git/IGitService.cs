
namespace GitMirror.Services.Git;

public interface IGitService
{
    public Task MirrorAsync(string sourceCloneUrl, string sourceUsername, string sourcePassword, string targetCloneUrl, string targetUsername, string targetPassword, CancellationToken cancellationToken = default);
}
