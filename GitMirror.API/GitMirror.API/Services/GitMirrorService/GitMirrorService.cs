using GitMirror.API.Data;
using System.Diagnostics;

namespace GitMirror.API.Services.GitMirrorService;

public class GitMirrorService(DatabaseContext db) : IGitMirrorService
{
    public async Task MirrorAsync(string sourceCloneUrl, string sourceUsername, string sourcePassword, string targetCloneUrl, string targetUsername, string targetPassword, CancellationToken cancellationToken = default)
    {
        var workDir = Path.Combine(Path.GetTempPath(), "git-mirror", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Inject credentials separately
        string sourceUrlWithCreds = InjectCredentials(sourceCloneUrl, sourceUsername, sourcePassword);
        string targetUrlWithCreds = InjectCredentials(targetCloneUrl, targetUsername, targetPassword);

        try
        {
            await RunGitAsync($"clone --mirror {sourceUrlWithCreds} .", workDir, cancellationToken);
            await RunGitAsync($"push --mirror {targetUrlWithCreds}", workDir, cancellationToken);
        }
        finally
        {
            if (Directory.Exists(workDir))
            {
                RemoveReadOnlyAttributes(workDir);
                Directory.Delete(workDir, recursive: true);
            }
        }
    }

    private void RemoveReadOnlyAttributes(string directory)
    {
        var directoryInfo = new DirectoryInfo(directory);

        foreach (var file in directoryInfo.GetFiles("*", SearchOption.AllDirectories))
        {
            file.Attributes = FileAttributes.Normal;
        }

        foreach (var dir in directoryInfo.GetDirectories("*", SearchOption.AllDirectories))
        {
            dir.Attributes = FileAttributes.Normal;
        }
    }

    private string InjectCredentials(string url, string username, string password)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            var builder = new UriBuilder(uri)
            {
                UserName = Uri.EscapeDataString(username),
                Password = Uri.EscapeDataString(password)
            };
            return builder.ToString();
        }
        throw new InvalidOperationException("Invalid URL");
    }

    private async Task RunGitAsync(string arguments, string workingDirectory, CancellationToken ct)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            },
            EnableRaisingEvents = true
        };

        process.Start();

        var stdOutTask = process.StandardOutput.ReadToEndAsync(ct);
        var stdErrTask = process.StandardError.ReadToEndAsync(ct);

        await Task.WhenAll(stdOutTask, stdErrTask, process.WaitForExitAsync(ct));

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException($"Git command failed: {stdErrTask.Result}");
        }
    }
}
