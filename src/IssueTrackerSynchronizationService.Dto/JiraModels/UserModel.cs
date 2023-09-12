namespace IssueTrackerSynchronizationService.Dto.JiraModels;

/// <summary>
/// Модель пользователя
/// </summary>
public class UserModel
{
    /// <summary>
    /// Почта
    /// </summary>
    public string EmailAddress { get; set; }

    /// <summary>
    /// Отображаемое имя
    /// </summary>
    public string DisplayName { get; set; }
}
