using IssueTrackerSynchronizationService.Dto.Base;
using Newtonsoft.Json;

namespace IssueTrackerSynchronizationService.Dto.RedmineModels;

/// <summary>
/// Модель статуса
/// </summary>
public class StatusModel : BaseModel
{
    /// <summary>
    /// Признак закрытия задачи
    /// </summary>
    [JsonProperty(PropertyName = "is_closed")]
    public bool IsClosed { get; set; }
}
