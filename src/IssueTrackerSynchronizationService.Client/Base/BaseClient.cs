using IssueTrackerSynchronizationService.Client.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace IssueTrackerSynchronizationService.Client.Base;

/// <summary>
/// Клиент
/// </summary>
public abstract class BaseClient
{
    protected Uri BaseUri { get; set; }

    /// <summary>
    /// Выполняет запрос.
    /// </summary>
    /// <typeparam name="TRequest">Запрос</typeparam>
    /// <typeparam name="TResult">Ответ</typeparam>
    /// <param name="httpMethod">Метод запроса</param>
    /// <param name="relativeUri">Относительный URI, который идет к основному</param>
    /// <param name="request">Данные, которые надо передать</param>
    /// <param name="authorizationString">Строка авторизации, если есть</param>
    /// <returns>Результат запроса данными типа <see cref="{TResult}"/></returns>
    internal async Task<RequestResult<TResult>> ExecuteRequestAsync<TRequest, TResult>(HttpMethod httpMethod, string relativeUri, TRequest request = default, string authorizationString = default)
    {
        try
        {
            using var client = CreateClient();

            var requestMessage = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(BaseUri, relativeUri),
                Content = new StringContent(JsonConvert.SerializeObject(request,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        },
                        Formatting = Formatting.Indented
                    }), Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrWhiteSpace(authorizationString))
                requestMessage.Headers.Add("Authorization", $"Basic {authorizationString}");

            return await HandleJsonAsync<TResult>(await client.SendAsync(requestMessage));
        }
        catch (Exception ex)
        {
            return HandleException<TResult>(ex, HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Возвращает модель с исключением.
    /// </summary>
    /// <typeparam name="TResult">Ответ.</typeparam>
    /// <param name="ex">Исключение.</param>
    /// <param name="code">Код ошибки.</param>
    /// <returns>Результат запроса с исключением.</returns>
    private static RequestResult<TResult> HandleException<TResult>(Exception ex, HttpStatusCode code) =>
        new()
        {
            Data = default,
            Error = ex.Message,
            StatusCode = code
        };

    /// <summary>
    /// Конвертирует в модель (если есть данные) и возвращает результат запроса
    /// </summary>
    /// <typeparam name="TResult">Результат</typeparam>
    /// <param name="result">HTTP ответ</param>
    /// <returns>Результат запроса с данными</returns>
    private static async Task<RequestResult<TResult>> HandleJsonAsync<TResult>(HttpResponseMessage result)
    {
        var strResult = await result.Content.ReadAsStringAsync();

        if (!result.IsSuccessStatusCode)
        {
            return HandleException<TResult>(new Exception(strResult), result.StatusCode);
        }

        var requestResult = new RequestResult<TResult>
        {
            Data = string.IsNullOrWhiteSpace(strResult) ? default : JsonConvert.DeserializeObject<TResult>(strResult),
            Error = null,
            StatusCode = result.StatusCode
        };

        return requestResult;
    }

    /// <summary>
    /// Возвращает новый экземпляр <see cref="HttpClient"/> без заголовков
    /// </summary>
    /// <returns><see cref="HttpClient"/>Без заголовков Accept</returns>
    private static HttpClient CreateClient()
    {
        var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();

        return client;
    }
}