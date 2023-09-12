namespace IssueTrackerSynchronizationService.Dto.Enums;

/// <summary>
/// Статусы задач в Jira
/// </summary>
public enum JiraStatuses
{
    /// <summary>
    /// В работе
    /// </summary>
    InWork = 3,

    /// <summary>
    /// Переоткрыт
    /// </summary>
    Rediscovered = 4,

    /// <summary>
    /// Решенные
    /// </summary>
    Solved = 5,

    /// <summary>
    /// Закрыт
    /// </summary>
    Closed = 6,

    /// <summary>
    /// В ревью
    /// </summary>
    InReview = 10907
}
