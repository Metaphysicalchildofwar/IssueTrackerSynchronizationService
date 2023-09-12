using Newtonsoft.Json;

namespace IssueTrackerSynchronizationService.Dto.JiraModels;

/// <summary>
/// Модель для полей задачи
/// </summary>
public class FieldsModel
{
    /// <summary>
    /// Тестировщик
    /// </summary>
    [JsonProperty(PropertyName = "customfield_11590")]
    public UserModel Tester { get; set; }

    /// <summary>
    /// Назначена
    /// </summary>
    public UserModel Assignee { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public StatusModel Status { get; set; }

    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Summary { get; set; }
}
