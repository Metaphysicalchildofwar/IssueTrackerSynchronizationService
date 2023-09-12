namespace IssueTrackerSynchronizationService.Dto.JiraModels;

/// <summary>
/// Модель задачи Jira
/// </summary>
public class JiraIssueModel
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Номер задачи
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Поля задачи
    /// </summary>
    public FieldsModel Fields { get; set; }
}
