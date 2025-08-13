using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CleanArchitecture_WebApi>("cleanarchitecture-webapi")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT","Test");

builder.Build().Run();
