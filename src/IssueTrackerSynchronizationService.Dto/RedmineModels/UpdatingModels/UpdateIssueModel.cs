using IssueTrackerSynchronizationService.Client.Model;

namespace IssueTrackerSynchronizationService.Dto.RedmineModels.UpdatingModels;

/// <summary>
/// Модель для обновления задачи
/// </summary>
public class UpdateIssueModel : ApiKeyModel
{
    /// <summary>
    /// Задача
    /// </summary>
    public IssueForUpdatingModel Issue { get; set; }
}
