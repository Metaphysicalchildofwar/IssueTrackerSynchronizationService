using IssueTrackerSynchronizationService.Dto.Enums;
using Newtonsoft.Json;

namespace IssueTrackerSynchronizationService.Dto.JiraModels;

/// <summary>
/// Модель статуса
/// </summary>
public class StatusModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id
    {
        get => (int)Status;
        set => Status = (JiraStatuses)value;
    }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Статус задачи
    /// </summary>
    [JsonIgnore]
    public JiraStatuses Status { get; set; }
}
