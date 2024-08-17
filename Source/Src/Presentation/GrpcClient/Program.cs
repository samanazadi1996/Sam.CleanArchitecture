using Grpc.Net.Client;
using GrpcClient;
using GrpcClient.Protos;

HttpClientHandler httpHandler = new HttpClientHandler()
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
};

using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });

var client = new ProductService.ProductServiceClient(channel);

while (true)
{
    Console.WriteLine("GetPagedListProduct = 0, GetProductById = 1, CreateProduct = 2, UpdateProduct = 3, DeleteProduct = 4");
    Console.Write("Enter RequestType :");

    var requestType = Console.ReadLine();

    try
    {
        if (requestType == "0")
            await GrpcRequestHandler.GetPagedListProductAsync(client);
        else if (requestType == "1")
            await GrpcRequestHandler.GetProductByIdAsync(client);
        else if (requestType == "2")
            await GrpcRequestHandler.CreateProductAsync(client);
        else if (requestType == "3")
            await GrpcRequestHandler.UpdateProductAsync(client);
        else if (requestType == "4")
            await GrpcRequestHandler.DeleteProductAsync(client);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine(new string('-', 50));
}