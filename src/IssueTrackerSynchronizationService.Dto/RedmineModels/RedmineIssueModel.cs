using IssueTrackerSynchronizationService.Dto.Base;
using Newtonsoft.Json;

namespace IssueTrackerSynchronizationService.Dto.RedmineModels;

/// <summary>
/// Модель задачи Redmine
/// </summary>
public class RedmineIssueModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Проект
    /// </summary>
    public BaseModel Project { get; set; }

    /// <summary>
    /// Внешний трекер
    /// </summary>
    public BaseModel Tracker { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public StatusModel Status { get; set; }

    /// <summary>
    /// Приоритет
    /// </summary>
    public BaseModel Priority { get; set; }

    /// <summary>
    /// Автор
    /// </summary>
    public BaseModel Author { get; set; }

    /// <summary>
    /// Исполнитель
    /// </summary>
    [JsonProperty(PropertyName = "assigned_to")]
    public BaseModel AssignedTo { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public BaseModel Category { get; set; }

    /// <summary>
    /// Тема
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
}