using IssueTrackerSynchronizationService.Api;
using IssueTrackerSynchronizationService.Client.Interfaces;
using IssueTrackerSynchronizationService.Client.TrackerClient;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IRedmineClient, RedmineClient>();
    })
    .Build();

host.Run();
