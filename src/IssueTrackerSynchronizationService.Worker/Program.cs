using IssueTrackerSynchronizationService.Api;
using IssueTrackerSynchronizationService.Client.Interfaces;
using IssueTrackerSynchronizationService.Client.TrackerClient;
using IssueTrackerSynchronizationService.Dal;
using IssueTrackerSynchronizationService.Dal.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IRedmineClient, RedmineClient>();
        services.AddSingleton<IService, SynchronizeService>();
    })
    .Build();

host.Run();
