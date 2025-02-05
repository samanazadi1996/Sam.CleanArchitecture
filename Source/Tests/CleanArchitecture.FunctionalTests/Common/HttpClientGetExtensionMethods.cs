using System.Text;
using System.Text.Json;

namespace CleanArchitecture.FunctionalTests.Common;

public static class HttpClientGetExtensionMethods
{
    static readonly JsonSerializerOptions DefaultJsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static async Task<T> GetAndDeserializeAsync<T>(this HttpClient client, string requestUri, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string text = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(text, DefaultJsonOptions)!;
    }
    public static async Task<T> PostAndDeserializeAsync<T>(this HttpClient client, string requestUri, object? model = null, string? token = null)
    {
        string jsonContent = JsonSerializer.Serialize(model, DefaultJsonOptions);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = content
        };

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);

        string text = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(text, DefaultJsonOptions)!;
    }
    public static async Task<T> PutAndDeserializeAsync<T>(this HttpClient client, string requestUri, object? model = null, string? token = null)
    {
        string jsonContent = JsonSerializer.Serialize(model, DefaultJsonOptions);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
        {
            Content = content
        };

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);

        string text = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(text, DefaultJsonOptions)!;
    }
    public static async Task<T> DeleteAndDeserializeAsync<T>(this HttpClient client, string requestUri, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);

        string text = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(text, DefaultJsonOptions)!;
    }

}
