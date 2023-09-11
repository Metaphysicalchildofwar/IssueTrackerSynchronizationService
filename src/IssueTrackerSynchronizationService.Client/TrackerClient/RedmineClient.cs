using IssueTrackerSynchronizationService.Client.Base;
using IssueTrackerSynchronizationService.Client.Interfaces;
using IssueTrackerSynchronizationService.Client.Model;
using IssueTrackerSynchronizationService.Dto.RedmineModels;
using IssueTrackerSynchronizationService.Dto.RedmineModels.UpdatingModels;
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
    private readonly ApiKeyModel ApiKey;

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
    public async Task<IEnumerable<IssueModel>> GetTrackedIssuesAsync()
    {
        var issues = await ExecuteRequestAsync<ApiKeyModel, RequestModel>(HttpMethod.Get, $"/issues.json?cf_39=*", ApiKey);
        //var issues = await ExecuteRequestAsync<ApiKeyModel, RequestModel>(HttpMethod.Get, $"/issues.json?issue_id={IssuesIds}", ApiKey); //для теста

        _logger.LogInformation(JsonConvert.SerializeObject(issues, Formatting.Indented));

        return issues?.Data?.Issues.Where(x => !x.Status.IsClosed);
    }


    /// <summary>
    /// Изменить задачу
    /// </summary>
    /// <param name="issue">Задача</param>
    /// <param name="assignedToId">Идентификатор, кому назначена</param>
    /// <param name="statusId">Идентификатор статуса</param>
    /// <returns>Результат изменения</returns>
    public async Task<bool> ChangeIssueAsync(IssueModel issue, int statusId, int assignedToId)
    {
        var resultUpdating = await ExecuteRequestAsync<UpdateIssueModel, RequestModel>(HttpMethod.Put, $"/issues/{issue.Id}.json",
            new() { Issue = new() { StatusId = statusId, AssignedToId = assignedToId }, Key = ApiKey.Key });

        _logger.LogInformation(JsonConvert.SerializeObject(resultUpdating, Formatting.Indented));

        return !string.IsNullOrWhiteSpace(resultUpdating.Error);
    }
}
