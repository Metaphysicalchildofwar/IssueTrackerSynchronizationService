using IssueTrackerSynchronizationService.Dto.RedmineModels;

namespace IssueTrackerSynchronizationService.Client.Interfaces;

/// <summary>
/// Интерфейс для Redmine
/// </summary>
public interface IRedmineClient: IClient<IssueModel>
{
    /// <summary>
    /// Изменить задачу
    /// </summary>
    /// <param name="issue">Задача</param>
    /// <param name="assignedToId">Идентификатор, кому назначена</param>
    /// <param name="statusId">Идентификатор статуса</param>
    /// <returns>Результат изменения</returns>
    Task<bool> ChangeIssueAsync(IssueModel issue, int statusId, int assignedToId);
}
