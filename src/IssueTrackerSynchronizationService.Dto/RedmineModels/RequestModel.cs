namespace IssueTrackerSynchronizationService.Dto.RedmineModels;

/// <summary>
/// Модель получения задач для отслеживания
/// </summary>
public class RequestModel
{
    /// <summary>
    /// Список задач
    /// </summary>
    public List<RedmineIssueModel> Issues { get; set; } = new();
}
