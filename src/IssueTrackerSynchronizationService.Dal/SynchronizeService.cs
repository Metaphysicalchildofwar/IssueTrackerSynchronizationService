using IssueTrackerSynchronizationService.Client.Interfaces;
using IssueTrackerSynchronizationService.Dal.Interfaces;
using IssueTrackerSynchronizationService.Dto.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace IssueTrackerSynchronizationService.Dal;

/// <summary>
/// Сервис синхронизации задач Redmine и Jira
/// </summary>
public class SynchronizeService : IService
{
    private readonly ILogger<SynchronizeService> _logger;
    private readonly IRedmineClient _redmineClient;
    private readonly IJiraClient _jiraClient;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Внешний идентификатор поля "Ссылка на внеш. трекер"
    /// </summary>
    private readonly string ExternalTrackerId;

    /// <summary>
    /// Список синхронизации статусов Jira и Redmine
    /// </summary>
    private readonly Dictionary<JiraStatuses, RedmineIssueStatuses> _statusMappingList = new()
    {
        [JiraStatuses.InWork] = RedmineIssueStatuses.InWork,
        [JiraStatuses.Rediscovered] = RedmineIssueStatuses.Debugging,
        [JiraStatuses.Solved] = RedmineIssueStatuses.Testing,
        //[JiraStatuses.Closed] = RedmineIssueStatuses.Closed,
        [JiraStatuses.InReview] = RedmineIssueStatuses.CodeReview
    };

    public SynchronizeService(ILogger<SynchronizeService> logger, IRedmineClient redmineClient, IJiraClient jiraClient, IConfiguration configuration)
    {
        _logger = logger;
        _redmineClient = redmineClient;
        _jiraClient = jiraClient;
        _configuration = configuration;
        ExternalTrackerId = _configuration.GetSection("Redmine:RedmineItemsIds:ExternalTrackerId").Value;
    }

    /// <summary>
    /// Синхронизировать задачи Redmine и Jira
    /// </summary>
    public async Task SynchronizeIssuesAsync()
    {
        try
        {
            var redmineIssues = await _redmineClient.GetTrackedIssuesAsync();

            await Parallel.ForEachAsync(redmineIssues, async (item, cancellationToken) =>
            {
                try
                {
                    var jiraIssueNumber = GetJiraIssueNumber(item.CustomFields.FirstOrDefault(x => x.Id.Equals(ExternalTrackerId)).Value as string);

                    if (!string.IsNullOrWhiteSpace(jiraIssueNumber))
                    {
                        var jiraIssue = await _jiraClient.GetTrackedIssueAsync(jiraIssueNumber);

                        if (jiraIssue != null)
                            await _redmineClient.ChangeIssueAsync(item, (int)_statusMappingList.FirstOrDefault(x => x.Key.Equals(jiraIssue.Fields.Status.Status)).Value);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    /// <summary>
    /// Получить номер задачи из Jira
    /// </summary>
    /// <param name="text">Ссылка на внеш. трекер</param>
    /// <returns>Номер задачи из Jira</returns>
    private string GetJiraIssueNumber(string text) => new Regex(_configuration.GetSection("Jira:RegularExpressionIssues").Value).Match(text).Value;
}