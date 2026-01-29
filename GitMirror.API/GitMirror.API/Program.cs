using GitMirror.API.Data;
using GitMirror.API.Helpers.Hangfire;
using GitMirror.API.Services.GitMirrorService;
using GitMirror.API.Services.HistoryService;
using GitMirror.API.Services.MirrorService;
using GitMirror.API.Services.OverviewService;
using GitMirror.API.Services.PlatformIntegrationsService;
using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps;
using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api;
using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.Bitbucket;
using GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api;
using GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.GitHub;
using GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api;
using GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.GitLab;
using GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api;
using GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api.Gateway;
using GitMirror.API.Services.PlatformMirrorService;
using GitMirror.API.Services.PlatformService;
using GitMirror.API.Services.RepositoryMirrorService;
using GitMirror.API.Services.RepositoryService;
using GitMirror.API.Services.SettingService;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Sinks.Network;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.TCPSink(builder.Configuration["Elastic:TcpSink"])
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot/GitMirror.UI/browser/";
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GitMirror.API", Version = "v1" });
});

builder.Services.AddHangfire(configuration =>
{
    configuration.UseSerilogLogProvider()
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(x =>
        {
            x.UseNpgsqlConnection(builder.Configuration.GetConnectionString("Postgres"));
        });
});

builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = 1;
});

builder.Services.AddControllers();

builder.Services.AddTransient<IHistoryService, HistoryService>();
builder.Services.AddTransient<IMirrorService, MirrorService>();
builder.Services.AddTransient<IRepositoryService, RepositoryService>();
builder.Services.AddTransient<IPlatformService, PlatformService>();
builder.Services.AddTransient<IOverviewService, OverviewService>();
builder.Services.AddTransient<ISettingService, SettingService>();

builder.Services.AddTransient<IPlatformMirrorService, PlatformMirrorService>();
builder.Services.AddTransient<IPlatformIntegrationServiceFactory, PlatformIntegrationServiceFactory>();

builder.Services.AddTransient<IRepositoryMirrorService, RepositoryMirrorService>();

builder.Services.AddTransient<IPlatformIntegrationService, AzureDevOpsService>();
builder.Services.AddTransient<IAzureDevOpsApiService, AzureDevOpsApiService>();
builder.Services.AddHttpClient<IAzureDevOpsGateway, AzureDevOpsGateway>();

builder.Services.AddTransient<IPlatformIntegrationService, GitLabService>();
builder.Services.AddTransient<IGitLabApiService, GitLabApiService>();
builder.Services.AddHttpClient<IGitLabGateway, GitLabGateway>();

builder.Services.AddTransient<IPlatformIntegrationService, GitHubService>();
builder.Services.AddTransient<IGitHubApiService, GitHubApiService>();
builder.Services.AddHttpClient<IGitHubGateway, GitHubGateway>();

builder.Services.AddTransient<IPlatformIntegrationService, BitbucketService>();
builder.Services.AddTransient<IBitbucketApiService, BitbucketApiService>();
builder.Services.AddHttpClient<IBitbucketGateway, BitbucketGateway>();

builder.Services.AddTransient<IGitMirrorService, GitMirrorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseSpaStaticFiles();

app.UseSerilogRequestLogging();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.UseHangfireDashboard("/hangfire");

app.UseSpa(spa =>
{
    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    await db.Database.MigrateAsync();
}
catch (Exception ex)
{
    Log.Logger.Information("An error occurred while migrating the database: {Message}", ex.Message);
}

if (!HangfireHelper.RecurringJobExists("Execute Platform Mirror"))
{
    RecurringJob.AddOrUpdate<IPlatformMirrorService>("Execute Platform Mirror", x => x.Execute(), Cron.Daily(2), new RecurringJobOptions());
}

if (!HangfireHelper.RecurringJobExists("Execute Repository Mirror"))
{
    RecurringJob.AddOrUpdate<IRepositoryMirrorService>("Execute Repository Mirror", x => x.Execute(), Cron.Daily(0), new RecurringJobOptions());
}

app.Run();