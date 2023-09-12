using IssueTrackerSynchronizationService.Client.Base;
using IssueTrackerSynchronizationService.Client.Interfaces;
using IssueTrackerSynchronizationService.Dto.JiraModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace IssueTrackerSynchronizationService.Client.TrackerClient;

/// <summary>
/// Клиент для Jira
/// </summary>
public class JiraClient : BaseClient, IJiraClient
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<RedmineClient> _logger;

    private readonly string AuthorizationString;

    public JiraClient(IConfiguration configuration, ILogger<RedmineClient> logger)
    {
        _configuration = configuration;
        _logger = logger;

        BaseUri = new Uri(_configuration.GetSection("Jira:BaseUri").Value);
        AuthorizationString = Convert.ToBase64String(
            Encoding.UTF8.GetBytes($"{_configuration.GetSection("Jira:Login").Value}:{_configuration.GetSection("Jira:Password").Value}"));
    }

    /// <summary>
    /// Получить отслеживаемую задачу
    /// </summary>
    /// <param name="issueNumber">Номер задачи</param>
    /// <returns>Задача</returns>
    public async Task<object> GetTrackedIssueAsync(string issueNumber)
    {
        var issue = await ExecuteRequestAsync<object, JiraIssueModel>(HttpMethod.Get, $"/rest/api/latest/issue/{issueNumber}", authorizationString: AuthorizationString);

        _logger.LogInformation(JsonConvert.SerializeObject(issue, Formatting.Indented));

        return issue;
    }
}
