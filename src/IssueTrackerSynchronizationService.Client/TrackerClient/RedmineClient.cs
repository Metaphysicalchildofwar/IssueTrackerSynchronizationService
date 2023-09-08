using IssueTrackerSynchronizationService.Client.Base;
using IssueTrackerSynchronizationService.Client.Interfaces;
using IssueTrackerSynchronizationService.Client.Model;
using IssueTrackerSynchronizationService.Dto.RedmineModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IssueTrackerSynchronizationService.Client.TrackerClient;

/// <summary>
/// Клиент для Redmine
/// </summary>
public class RedmineClient : BaseClient, IRedmineClient
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<RedmineClient> _logger;

    /// <summary>
    /// Идентификаторы задач из Redmine
    /// </summary>
    private readonly string IssuesIds;

    /// <summary>
    /// Ключ доступа к API
    /// </summary>
    private readonly RedmineApiKeyModel ApiKey;

    public RedmineClient(IConfiguration configuration, ILogger<RedmineClient> logger)
    {
        _configuration = configuration;
        _logger = logger;

        BaseUri = new Uri(_configuration.GetSection("Redmine:BaseUri").Value);
        IssuesIds = string.Join(",", _configuration.GetSection("Redmine:TaskNumbers").Get<string[]>());
        ApiKey = new() { Key = _configuration.GetSection("Redmine:ApiKey").Value };
    }

    /// <summary>
    /// Получить отслеживаемые задачи
    /// </summary>
    /// <returns>Список задач</returns>
    public async Task<IEnumerable<RedmineIssueModel>> GetTrackedIssues()
    {
        var issues = await ExecuteRequestAsync<RedmineApiKeyModel, RequestModel>(HttpMethod.Get, $"/issues.json?issue_id={IssuesIds}", ApiKey);

        _logger.LogInformation(JsonConvert.SerializeObject(issues, Formatting.Indented));

        return issues?.Data?.Issues.Where(x => !x.Status.IsClosed);
    }
}
