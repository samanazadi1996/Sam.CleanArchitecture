using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using TestTemplate.Application.Interfaces;
using TestTemplate.Application.Wrappers;

namespace TestTemplate.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class FileController(IFileManagerService fileManagerService) : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetFile(string name)
        {
            var bytes = await fileManagerService.Download(name);

            return File(bytes, MediaTypeNames.Application.Octet, name);
        }

        [HttpPost]
        public async Task<BaseResult<string>> UploadFile(string name, IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                await fileManagerService.Create(name, memoryStream.ToArray());
                await fileManagerService.SaveChangesAsync();

                return new(name);
            }
        }
    }
}