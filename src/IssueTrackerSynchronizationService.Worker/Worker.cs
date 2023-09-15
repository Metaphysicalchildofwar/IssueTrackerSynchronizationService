using IssueTrackerSynchronizationService.Dal.Interfaces;

namespace IssueTrackerSynchronizationService.Api;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IService _synchronizeService;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger, IService synchronizeService, IConfiguration configuration)
    {
        _logger = logger;
        _synchronizeService = synchronizeService;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int.TryParse(_configuration.GetSection("Timer").Value, out int timer);

        if (timer == 0)
        {
            var errorText = "Не задана периодичность вызова";

            _logger.LogError(errorText);
            throw new Exception(errorText);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            await _synchronizeService.SynchronizeIssuesAsync();

            await Task.Delay(TimeSpan.FromMinutes(timer), stoppingToken);
        }
    }
}