using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Endpoints;

public class FileEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapGet(GetFile);
        builder.MapPost(UploadFile);
    }

    async Task<IResult> GetFile(IFileManagerService fileManagerService, string name)
    {
        var bytes = await fileManagerService.Download(name);

        return Results.File(bytes, MediaTypeNames.Application.Octet, name);
    }

    async Task<BaseResult<string>> UploadFile(IFileManagerService fileManagerService, string name, IFormFile file)
    {
        using var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream);
        await fileManagerService.Create(name, memoryStream.ToArray());
        await fileManagerService.SaveChangesAsync();

        return name;
    }
}
