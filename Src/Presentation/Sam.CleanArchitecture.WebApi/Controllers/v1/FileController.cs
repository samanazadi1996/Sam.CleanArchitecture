using Microsoft.AspNetCore.Mvc;
using Sam.CleanArchitecture.Application.Interfaces;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class FileController : BaseApiController
    {
        private readonly IFileManagerService fileManagerService;

        public FileController(IFileManagerService fileManagerService)
        {
            this.fileManagerService = fileManagerService;
        }

        [HttpGet("GetFile")]
        public async Task<IActionResult> Get(string name)
        {
            var bytes = await fileManagerService.Download(name);

            return File(bytes, MediaTypeNames.Application.Octet, name);
        }

    }

}