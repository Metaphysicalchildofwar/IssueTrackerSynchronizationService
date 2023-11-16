using IssueTrackerSynchronizationService.Dto.JiraModels;

namespace IssueTrackerSynchronizationService.Client.Interfaces;

/// <summary>
/// Интерфейс для Jira
/// </summary>
public interface IJiraClient
{
    /// <summary>
    /// Получить отслеживаемую задачу
    /// </summary>
    /// <param name="issueNumber">Номер задачи</param>
    /// <returns>Задача</returns>
    Task<JiraIssueModel> GetTrackedIssueAsync(string issueNumber);
}
