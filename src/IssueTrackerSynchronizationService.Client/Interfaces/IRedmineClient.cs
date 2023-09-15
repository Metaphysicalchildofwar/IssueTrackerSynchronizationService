using IssueTrackerSynchronizationService.Dto.RedmineModels;

namespace IssueTrackerSynchronizationService.Client.Interfaces;

/// <summary>
/// Интерфейс для Redmine
/// </summary>
public interface IRedmineClient
{
    /// <summary>
    /// Получить отслеживаемые задачи
    /// </summary>
    /// <returns>Список задач</returns>
    Task<IEnumerable<RedmineIssueModel>> GetTrackedIssuesAsync();

    /// <summary>
    /// Изменить задачу
    /// </summary>
    /// <param name="issue">Задача</param>
    /// <param name="assignedToId">Идентификатор, кому назначена</param>
    /// <param name="statusId">Идентификатор статуса</param>
    Task ChangeIssueAsync(RedmineIssueModel issue, int statusId, int assignedToId);
}
