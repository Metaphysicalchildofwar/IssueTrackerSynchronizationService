﻿namespace IssueTrackerSynchronizationService.Dto.RedmineModels;

/// <summary>
/// Модель получения задач для отслеживания
/// </summary>
public class RequestModel
{
    /// <summary>
    /// Список задач
    /// </summary>
    public List<IssueModel> Issues { get; set; } = new();
}
