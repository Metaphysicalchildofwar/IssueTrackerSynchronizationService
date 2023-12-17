using System.Net;

namespace IssueTrackerSynchronizationService.Dto.ClientModels;

/// <summary>
/// Базовый результат запроса.
/// </summary>
public class BaseRequestResult
{
    /// <summary>
    /// Код состояния.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    public string Error { get; set; }
}
