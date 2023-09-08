namespace IssueTrackerSynchronizationService.Client.Model;

/// <summary>
/// Результат запроса с данными.
/// </summary>
/// <typeparam name="TResult">Тип возвращаемых данных.</typeparam>
public class RequestResult<TResult> : BaseRequestResult
{
    /// <summary>
    /// Данные.
    /// </summary>
    public TResult Data { get; set; }
}
