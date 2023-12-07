using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LMSystem.Services
{
    public class FileHandlerService : IFileHandlerService
    {
        public async Task<string> UploadFile(IFormFile file, string fileName, string fileType)
        {
            if (file == null || file.Length == 0)
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", fileType, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "Success";
        }

        public IFormFile ShowFile(string fileName, string fileType)
        {
            if (fileName == null)
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", fileType, fileName);

            var stream = new FileStream(path, FileMode.Open);

            var file = new FormFile(stream, 0, stream.Length, null, fileName);

            return file;
        }

        public async Task<FileInfor> DownloadFile(string fileName, string fileType)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", fileType, fileName);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            //File(file, contentType, Path.GetFileName(path));

            var fileInfor = new FileInfor
            {
                Bytes = bytes,
                ContentType = contentType,
                FilePath = path
            };

            return fileInfor;
        }
    }
}
