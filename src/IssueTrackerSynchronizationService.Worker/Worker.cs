using IssueTrackerSynchronizationService.Dal.Interfaces;

namespace IssueTrackerSynchronizationService.Api;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IService _synchronizeService;

    public Worker(ILogger<Worker> logger, IService synchronizeService)
    {
        _logger = logger;
        _synchronizeService = synchronizeService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(1000, stoppingToken);
                await _synchronizeService.SynchronizeIssuesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
        }
    }
}