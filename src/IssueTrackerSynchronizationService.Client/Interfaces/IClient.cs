namespace IssueTrackerSynchronizationService.Client.Interfaces;

/// <summary>
/// Интерфейс клиента
/// </summary>
public interface IClient<TResult>
{

    /// <summary>
    /// Получить отслеживаемые задачи
    /// </summary>
    /// <returns>Список задач</returns>
    Task<IEnumerable<TResult>> GetTrackedIssuesAsync();
}
