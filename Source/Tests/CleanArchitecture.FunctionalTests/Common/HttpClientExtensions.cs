using System.Text;
using System.Text.Json;

namespace CleanArchitecture.FunctionalTests.Common;

public static class HttpClientExtensions
{
    private static readonly JsonSerializerOptions DefaultJsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static async Task<T> GetAndDeserializeAsync<T>(this HttpClient client, string requestUri, string? token = null)
        => await client.BuildAndSendRequest<T>(HttpMethod.Get, requestUri, token);

    public static async Task<T> PostAndDeserializeAsync<T>(this HttpClient client, string requestUri, object? model = null, string? token = null)
        => await client.BuildAndSendRequest<T>(HttpMethod.Post, requestUri, token, model);

    public static async Task<T> PutAndDeserializeAsync<T>(this HttpClient client, string requestUri, object? model = null, string? token = null)
        => await client.BuildAndSendRequest<T>(HttpMethod.Put, requestUri, token, model);

    public static async Task<T> DeleteAndDeserializeAsync<T>(this HttpClient client, string requestUri, string? token = null)
        => await client.BuildAndSendRequest<T>(HttpMethod.Delete, requestUri, token);

    private static async Task<T> BuildAndSendRequest<T>(this HttpClient client, HttpMethod method, string requestUri, string? token, object? model = null)
    {
        var request = new HttpRequestMessage(method, requestUri);

        if (model is not null)
        {
            var json = JsonSerializer.Serialize(model, DefaultJsonOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new("Bearer", token);

        HttpResponseMessage response = await client.SendAsync(request);

        string text = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(text, DefaultJsonOptions)!;
    }

}
