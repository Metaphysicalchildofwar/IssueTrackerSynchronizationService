using IssueTrackerSynchronizationService.Client.Interfaces;
using IssueTrackerSynchronizationService.Dal.Interfaces;
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
    private readonly string ExternalTrackerId;

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
                var jiraIssueName = GetJiraIssueNumber(item.CustomFields.FirstOrDefault(x => x.Id.Equals(ExternalTrackerId)).Value as string);

                var jiraIssue = await _jiraClient.GetTrackedIssueAsync(jiraIssueName);

                //сопоставить статусы, узнать, на какой статус менять в редмайне и переводить на тестеров или обратно на разраба (если надо)
                // нужно как-то искать пользователя
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace);
        }
    }

    /// <summary>
    /// Получить номер задачи из Jira
    /// </summary>
    /// <param name="text">Ссылка на внеш. трекер</param>
    /// <returns>Номер задачи из Jira</returns>
    private string GetJiraIssueNumber(string text)
        => new Regex(_configuration.GetSection("Jira:RegularExpressionIssues").Value).Match(text).Value;
}