namespace IssueTrackerSynchronizationService.Dal.Interfaces;

/// <summary>
/// Интерфейс для сервисов
/// </summary>
public interface IService
{
    /// <summary>
    /// Синхронизировать задачи Redmine и Jira
    /// </summary>
    Task SynchronizeIssuesAsync();
}
