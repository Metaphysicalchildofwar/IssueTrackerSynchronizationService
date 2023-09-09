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
    private readonly IConfiguration _configuration;

    public SynchronizeService(ILogger<SynchronizeService> logger, IRedmineClient redmineClient, IConfiguration configuration)
    {
        _logger = logger;
        _redmineClient = redmineClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Синхронизировать задачи Redmine и Jira
    /// </summary>
    public async Task SynchronizeIssuesAsync()
    {
        var redmineIssues = await _redmineClient.GetTrackedIssuesAsync();

        foreach (var issue in redmineIssues)
        {
            var jiraIssueName = GetJiraIssueName(issue.CustomFields.FirstOrDefault(x => x.Name == "Ссылка на внеш.трекер").Value as string);

            //var jiraIssueName = GetJiraIssueName(issue.Description);

        }
    }

    /// <summary>
    /// Получить наименование задачи из Jira
    /// </summary>
    /// <param name="text">Ссылка на внеш. трекер</param>
    /// <returns>Наименование задачи из Jira</returns>
    private string GetJiraIssueName(string text)
    {
        return new Regex(_configuration.GetSection("Jira:RegularExpressionIssues").Value).Match(text).Value;
    }
}