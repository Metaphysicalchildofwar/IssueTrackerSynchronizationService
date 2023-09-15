using Newtonsoft.Json;

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

    /// <summary>
    /// Количество записей.
    /// </summary>
    [JsonProperty(PropertyName = "total_count")]
    public int TotalCount { get; set; }
}
