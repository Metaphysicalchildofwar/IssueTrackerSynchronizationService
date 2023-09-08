using Newtonsoft.Json;

namespace IssueTrackerSynchronizationService.Dto.RedmineModels.UpdatingModels;

/// <summary>
/// Модель задачи для обновления
/// </summary>
public class IssueForUpdatingModel
{
    /// <summary>
    /// Идентификатор статуса
    /// </summary>
    [JsonProperty(PropertyName = "status_id")]
    public int StatusId { get; set; }

    /// <summary>
    /// Идентификатор, кому назначена
    /// </summary>
    [JsonProperty(PropertyName = "assigned_to_id")]
    public int AssignedToId { get; set; }
}
