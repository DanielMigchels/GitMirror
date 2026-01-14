
namespace GitMirror.API.Services.GitMirrorService;

public interface IGitMirrorService
{
    public Task MirrorAsync(string sourceCloneUrl, string sourceUsername, string sourcePassword, string targetCloneUrl, string targetUsername, string targetPassword, CancellationToken cancellationToken = default);
}
