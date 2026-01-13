using GitMirror.Options;
using GitMirror.Services.Git;
using GitMirror.Services.GitMirror;
using GitMirror.Services.GitPlatforms;
using GitMirror.Services.GitPlatforms.AzureDevOps;
using GitMirror.Services.GitPlatforms.AzureDevOps.Api;
using GitMirror.Services.GitPlatforms.AzureDevOps.Api.Gateway;
using GitMirror.Services.GitPlatforms.GitLab;
using GitMirror.Services.GitPlatforms.GitLab.Api;
using GitMirror.Services.GitPlatforms.GitLab.Api.Gateway;
using GitMirror.Services.GitPlatforms.GitHub;
using GitMirror.Services.GitPlatforms.GitHub.Api;
using GitMirror.Services.GitPlatforms.GitHub.Api.Gateway;
using GitMirror.Services.GitPlatforms.Bitbucket;
using GitMirror.Services.GitPlatforms.Bitbucket.Api;
using GitMirror.Services.GitPlatforms.Bitbucket.Api.Gateway;
using GitMirror.Services.RepositoryMirror;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    // .WriteTo.TCPSink(builder.Configuration["Elastic:TcpSink"])
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.Configure<GitPlatformSettings>("AzureDevOps", builder.Configuration.GetSection("GitPlatforms:AzureDevOps"));
builder.Services.Configure<GitPlatformSettings>("GitLab", builder.Configuration.GetSection("GitPlatforms:GitLab"));
builder.Services.Configure<GitPlatformSettings>("GitHub", builder.Configuration.GetSection("GitPlatforms:GitHub"));
builder.Services.Configure<GitPlatformSettings>("Bitbucket", builder.Configuration.GetSection("GitPlatforms:Bitbucket"));

builder.Services.AddTransient<IRepositoryMirrorService, RepositoryMirrorService>();
builder.Services.AddTransient<IGitPlatformServiceFactory, GitPlatformServiceFactory>();

builder.Services.AddTransient<IGitService, GitService>();

builder.Services.AddTransient<IGitPlatformService, AzureDevOpsService>();
builder.Services.AddTransient<IAzureDevOpsApiService, AzureDevOpsApiService>();
builder.Services.AddHttpClient<IAzureDevOpsGateway, AzureDevOpsGateway>();

builder.Services.AddTransient<IGitPlatformService, GitLabService>();
builder.Services.AddTransient<IGitLabApiService, GitLabApiService>();
builder.Services.AddTransient<IGitLabGateway, GitLabGateway>();

builder.Services.AddTransient<IGitPlatformService, GitHubService>();
builder.Services.AddTransient<IGitHubApiService, GitHubApiService>();
builder.Services.AddHttpClient<IGitHubGateway, GitHubGateway>();

builder.Services.AddTransient<IGitPlatformService, BitbucketService>();
builder.Services.AddTransient<IBitbucketApiService, BitbucketApiService>();
builder.Services.AddHttpClient<IBitbucketGateway, BitbucketGateway>();

builder.Services.AddHangfire(configuration =>
{
    configuration.UseSerilogLogProvider().UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseInMemoryStorage();
});

builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = 1;
});

var app = builder.Build();

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[]
    {
        new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
        {
            RequireSsl = false,
            SslRedirect = false,
            LoginCaseSensitive = false,
            Users =
            [
                new BasicAuthAuthorizationUser
                {
                    Login = "admin",
                    PasswordClear = "admin"
                }
            ]
        })
    }
});

RecurringJob.AddOrUpdate<IRepositoryMirrorService>("Execute Git Repository Mirror", x => x.Execute(), Cron.Daily(), new RecurringJobOptions());

app.MapControllers();

app.Run();