using System.Text;
using System.Text.Json;

namespace CleanArchitecture.FunctionalTests.Common
{
    public static class HttpClientGetExtensionMethods
    {
        static JsonSerializerOptions DefaultJsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<T> GetAndDeserializeAsync<T>(this HttpClient client, string requestUri)
        {
            HttpResponseMessage response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            string text = await response.Content.ReadAsStringAsync();


#pragma warning disable CS8603 // Possible null reference return.
            return JsonSerializer.Deserialize<T>(text, DefaultJsonOptions);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static async Task<T> PostAndDeserializeAsync<T>(this HttpClient client, string requestUri, object? model = null)
        {
            string jsonContent = JsonSerializer.Serialize(model, DefaultJsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the POST request
            HttpResponseMessage response = await client.PostAsync(requestUri, content);
            response.EnsureSuccessStatusCode();
            string text = await response.Content.ReadAsStringAsync();

#pragma warning disable CS8603 // Possible null reference return.
            return JsonSerializer.Deserialize<T>(text, DefaultJsonOptions);
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
