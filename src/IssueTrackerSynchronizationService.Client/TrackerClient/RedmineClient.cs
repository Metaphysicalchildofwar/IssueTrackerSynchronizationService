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
    private readonly string ExternalTrackerId;
    private readonly string ProjectMisId;

    /// <summary>
    /// Ключ доступа к API
    /// </summary>
    private readonly ApiKeyModel ApiKey;

    public RedmineClient(IConfiguration configuration, ILogger<RedmineClient> logger)
    {
        _configuration = configuration;
        _logger = logger;

        BaseUri = new Uri(_configuration.GetSection("Redmine:BaseUri").Value);
        ApiKey = new() { Key = _configuration.GetSection("Redmine:ApiKey").Value };
        ExternalTrackerId = _configuration.GetSection("Redmine:RedmineItemsIds:ExternalTrackerId").Value;
        ProjectMisId = _configuration.GetSection("Redmine:RedmineItemsIds:ProjectMisId").Value;
    }

    /// <summary>
    /// Получить отслеживаемые задачи
    /// </summary>
    /// <returns>Список задач</returns>
    public async Task<IEnumerable<RedmineIssueModel>> GetTrackedIssuesAsync()
    {
        var limit = 100;
        var issues = new List<RedmineIssueModel>();

        var totalCount = await GetIssues(limit, issues);

        while (totalCount > issues.Count)
        {
            await GetIssues(limit, issues);
        }

        return issues;
    }


    /// <summary>
    /// Изменить задачу
    /// </summary>
    /// <param name="issue">Задача</param>
    /// <param name="assignedToId">Идентификатор, кому назначена</param>
    /// <param name="statusId">Идентификатор статуса</param>
    /// <returns>Результат изменения</returns>
    public async Task<bool> ChangeIssueAsync(RedmineIssueModel issue, int statusId, int assignedToId)
    {
        var resultUpdating = await ExecuteRequestAsync<UpdateIssueModel, RequestModel>(HttpMethod.Put, $"/issues/{issue.Id}.json",
            new() { Issue = new() { StatusId = statusId, AssignedToId = assignedToId }, Key = ApiKey.Key });

        _logger.LogInformation(JsonConvert.SerializeObject(resultUpdating, Formatting.Indented));

        return !string.IsNullOrWhiteSpace(resultUpdating.Error);
    }

    /// <summary>
    /// Получить задачи
    /// </summary>
    /// <param name="limit">Количество задач</param>
    /// <param name="issues">Список полученных задач</param>
    /// <returns>Количество всех задач по фильтру (нужно для первого получения и проверки, нужно ли получать еще задачи)</returns>
    private async Task<int> GetIssues(int limit, List<RedmineIssueModel> issues)
    {
        var issuesRes = await ExecuteRequestAsync<ApiKeyModel, RequestModel>(HttpMethod.Get,
            $"/issues.json?cf_{ExternalTrackerId}=*&project_id={ProjectMisId}&status_id=open&offset={issues.Count}&limit={limit}", ApiKey);

        _logger.LogInformation(JsonConvert.SerializeObject(issuesRes, Formatting.Indented));

        issues.AddRange(issuesRes?.Data?.Issues);

        return issuesRes.Data.TotalCount;
    }
}
