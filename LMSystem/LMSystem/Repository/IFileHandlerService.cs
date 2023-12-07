using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Repository
{
    public interface IFileHandlerService
    {
        Task<FileInfor> DownloadFile(string fileName, string fileType);
        IFormFile ShowFile(string fileName, string fileType);
        Task<string> UploadFile(IFormFile file, string fileName, string fileType);
    }
}