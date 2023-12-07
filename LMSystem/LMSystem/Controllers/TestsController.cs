using LMSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        public readonly IFileHandlerService _fileHandlerService;

        public TestsController(IFileHandlerService fileHandlerService)
        {
            _fileHandlerService = fileHandlerService;
        }

        [HttpPost("Upload File")]
        public async Task<IActionResult> UploadFile(IFormFile file, string fileName, string fileType)
        {
            var result  = await _fileHandlerService.UploadFile(file, fileName, fileType);

            return Content(result);
        }

        [HttpPost("Show file")]
        public IFormFile ShowFile(string filename, string fileType)
        {
            return _fileHandlerService.ShowFile(filename, fileType);
        }

        [HttpGet("Dowload File")]
        public async Task<IActionResult> DownloadFile(string fileName, string fileType)
        {
            var fileInfor = await _fileHandlerService.DownloadFile(fileName, fileType);

            return File(fileInfor.Bytes, fileInfor.ContentType, fileInfor.FilePath);
        }
    }
}
