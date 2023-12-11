using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LMSystem.Services
{
    public class FileHandlerService : IFileHandlerService
    {
        public async Task<string> UploadFile(IFormFile file, string fileName, string folderType)
        {
            if (file == null || file.Length == 0)
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", folderType, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return path;
        }

        public IFormFile ShowFile(string fileName, string folderType)
        {
            if (fileName == null)
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", folderType, fileName);

            var stream = new FileStream(path, FileMode.Open);

            var file = new FormFile(stream, 0, stream.Length, null, fileName);

            return file;
        }

        public string ChangeFileName(string oldName, string newName, string folderType)
        {
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", folderType, oldName);

            if (!System.IO.File.Exists(oldPath))
                return "File doesn't exist!";

            var newPath = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", folderType, newName);

            System.IO.File.Move(oldPath, newPath);

            return "Change successful!";
        }

        public async Task<FileInfor> DownloadFile(string fileName, string folderType)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", folderType, fileName);

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

        public string DeleteFile(string fileName, string folderType)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Storage", folderType, fileName);

            if (!System.IO.File.Exists(path))
                return "Doesn't exist!";

            System.IO.File.Delete(path);

            return "Delete success!";
        }
    }
}
