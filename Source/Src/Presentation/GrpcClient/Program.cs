using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient.Protos;
using System.Text.Json;

// Disable SSL certificate validation
var httpHandler = new HttpClientHandler();
httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

// Create gRPC channel with SSL validation disabled
using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });

// Create the gRPC client
var client = new ProductService.ProductServiceClient(channel);

// Create the request with pagination parameters
var request = new GetPagedListProductRequest { PageNumber = 1, PageSize = 10 };

// Prompt user to input Bearer token
Console.Write("Enter Bearer token: ");
var bearerToken = Console.ReadLine(); // Read the Bearer token from the console

// Create metadata with the Bearer token
var headers = string.IsNullOrEmpty(bearerToken) ? null : new Metadata
{
    { "Authorization", $"Bearer {bearerToken}" }
};

// Infinite loop to repeatedly make requests
while (true)
{
    // Wait for 3 seconds between requests
    Thread.Sleep(3000);

    try
    {
        // Record the start time
        var start = DateTime.Now;

        // Make the gRPC call asynchronously with the metadata
        var response = await client.GetPagedListProductAsync(request, headers);

        // Log the request time, response time, and the response data
        var end = DateTime.Now;
        var elapsed = end - start;

        Console.WriteLine($"Request Start Time: {start:HH:mm:ss.fff}");
        Console.WriteLine($"Response End Time: {end:HH:mm:ss.fff}");
        Console.WriteLine($"Elapsed Time: {elapsed.TotalMilliseconds} ms");
        Console.WriteLine("Response:");
        Console.WriteLine(JsonSerializer.Serialize(response));
        Console.WriteLine(new string('-', 50)); // 50 dashes to create a clear separation line
    }
    catch (Exception ex)
    {
        // Log any exceptions that occur during the request
        Console.WriteLine($"Error: {ex.Message}");
    }
}
