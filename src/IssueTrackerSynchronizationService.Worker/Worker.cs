using IssueTrackerSynchronizationService.Client.Interfaces;
using IssueTrackerSynchronizationService.Dto.RedmineModels;

namespace IssueTrackerSynchronizationService.Api;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRedmineClient _redmineClient;

    public Worker(ILogger<Worker> logger, IRedmineClient redmineClient)
    {
        _logger = logger;
        _redmineClient = redmineClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(1000, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
        }
    }
}