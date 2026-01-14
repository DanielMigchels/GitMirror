using GitMirror.API.Options;
using GitMirror.API.Services.GitMirrorService;
using GitMirror.API.Services.PlatformMirrorService;
using GitMirror.API.Services.PlatformIntegrationsService;
using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps;
using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api;
using GitMirror.API.Services.PlatformIntegrationsService.AzureDevOps.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.GitLab;
using GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api;
using GitMirror.API.Services.PlatformIntegrationsService.GitLab.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.GitHub;
using GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api;
using GitMirror.API.Services.PlatformIntegrationsService.GitHub.Api.Gateway;
using GitMirror.API.Services.PlatformIntegrationsService.Bitbucket;
using GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api;
using GitMirror.API.Services.PlatformIntegrationsService.Bitbucket.Api.Gateway;
using Hangfire;
using Serilog;
using Microsoft.OpenApi;
using Serilog.Sinks.Network;
using GitMirror.API.Data;
using Microsoft.EntityFrameworkCore;

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
    configuration.RootPath = "wwwroot/";
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GitMirror.API", Version = "v1" });
});

builder.Services.AddHangfire(configuration =>
{
    configuration.UseSerilogLogProvider().UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseInMemoryStorage();
});

builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = 1;
});

builder.Services.AddControllers();

builder.Services.Configure<PlatformSettings>("AzureDevOps", builder.Configuration.GetSection("Platforms:AzureDevOps"));
builder.Services.Configure<PlatformSettings>("GitLab", builder.Configuration.GetSection("Platforms:GitLab"));
builder.Services.Configure<PlatformSettings>("GitHub", builder.Configuration.GetSection("Platforms:GitHub"));
builder.Services.Configure<PlatformSettings>("Bitbucket", builder.Configuration.GetSection("Platforms:Bitbucket"));

builder.Services.AddTransient<IPlatformMirrorService, PlatformMirrorService>();
builder.Services.AddTransient<IPlatformServiceFactory, PlatformServiceFactory>();

builder.Services.AddTransient<IGitMirrorService, GitMirrorService>();

builder.Services.AddTransient<IPlatformService, AzureDevOpsService>();
builder.Services.AddTransient<IAzureDevOpsApiService, AzureDevOpsApiService>();
builder.Services.AddHttpClient<IAzureDevOpsGateway, AzureDevOpsGateway>();

builder.Services.AddTransient<IPlatformService, GitLabService>();
builder.Services.AddTransient<IGitLabApiService, GitLabApiService>();
builder.Services.AddHttpClient<IGitLabGateway, GitLabGateway>();

builder.Services.AddTransient<IPlatformService, GitHubService>();
builder.Services.AddTransient<IGitHubApiService, GitHubApiService>();
builder.Services.AddHttpClient<IGitHubGateway, GitHubGateway>();

builder.Services.AddTransient<IPlatformService, BitbucketService>();
builder.Services.AddTransient<IBitbucketApiService, BitbucketApiService>();
builder.Services.AddHttpClient<IBitbucketGateway, BitbucketGateway>();

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


app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    /*
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
    */
});

app.UseSpa(spa =>
{
    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
await db.Database.MigrateAsync();

RecurringJob.AddOrUpdate<IPlatformMirrorService>("Execute Git Repository Mirror", x => x.Execute(), Cron.Daily(), new RecurringJobOptions());

app.Run();