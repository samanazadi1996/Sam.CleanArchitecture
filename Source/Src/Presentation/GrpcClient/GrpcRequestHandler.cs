using Grpc.Core;
using GrpcClient.Protos;
using System.Text.Json;

namespace GrpcClient
{
    internal class GrpcRequestHandler
    {
        public static async Task GetPagedListProductAsync(ProductService.ProductServiceClient client)
        {
            var request = new GetPagedListProductRequest
            {
                PageNumber = int.Parse(GetValueFromUser("PageNumber") ?? "1"),
                PageSize = int.Parse(GetValueFromUser("PageSize") ?? "10")
            };

            var start = DateTime.Now;
            var response = await client.GetPagedListProductAsync(request);

            Log(start, DateTime.Now, response);
        }
        public static async Task GetProductByIdAsync(ProductService.ProductServiceClient client)
        {
            var request = new GrpcBaseRequestWithIdParameter
            {
                Id = long.Parse(GetValueFromUser("Id") ?? "0")
            };

            var start = DateTime.Now;
            var response = await client.GetProductByIdAsync(request);

            Log(start, DateTime.Now, response);
        }
        public static async Task CreateProductAsync(ProductService.ProductServiceClient client)
        {
            var request = new CreateProductRequest()
            {
                Name = GetValueFromUser("Name"),
                BarCode = GetValueFromUser("BarCode"),
                Price = double.Parse(GetValueFromUser("Price") ?? "0")
            };
            var headers = new Metadata
            {
                { "Authorization", $"Bearer {GetValueFromUser("Bearer token")}" }
            };

            var start = DateTime.Now;
            var response = await client.CreateProductAsync(request, headers);

            Log(start, DateTime.Now, response);
        }
        public static async Task UpdateProductAsync(ProductService.ProductServiceClient client)
        {
            var request = new UpdateProductRequest()
            {
                Id = long.Parse(GetValueFromUser("Id") ?? "0"),
                Name = GetValueFromUser("Name"),
                BarCode = GetValueFromUser("BarCode"),
                Price = double.Parse(GetValueFromUser("Price") ?? "0")
            };

            var headers = new Metadata
            {
                { "Authorization", $"Bearer {GetValueFromUser("Bearer token")}" }
            };

            var start = DateTime.Now;
            var response = await client.UpdateProductAsync(request, headers);

            Log(start, DateTime.Now, response);
        }
        public static async Task DeleteProductAsync(ProductService.ProductServiceClient client)
        {
            var request = new GrpcBaseRequestWithIdParameter()
            {
                Id = long.Parse(GetValueFromUser("Id") ?? "0")
            };

            var headers = new Metadata
            {
                { "Authorization", $"Bearer {GetValueFromUser("Bearer token")}" }
            };

            var start = DateTime.Now;
            var response = await client.DeleteProductAsync(request, headers);

            Log(start, DateTime.Now, response);
        }
        private static void Log(DateTime start, DateTime end, object response)
        {
            var elapsed = end - start;

            Console.WriteLine($"Request Start Time: {start:HH:mm:ss.fff}");
            Console.WriteLine($"Response End Time: {end:HH:mm:ss.fff}");
            Console.WriteLine($"Elapsed Time: {elapsed.TotalMilliseconds} ms");
            Console.WriteLine("Response:");
            Console.WriteLine(JsonSerializer.Serialize(response));
        }
        private static string? GetValueFromUser(string text)
        {
            Console.Write($"Enter {text} :");
            return Console.ReadLine();
        }
    }
}
