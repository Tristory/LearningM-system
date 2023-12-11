using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Repository
{
    public interface IFileHandlerService
    {
        string ChangeFileName(string oldName, string newName, string folderType);
        string DeleteFile(string fileName, string folderType);
        Task<FileInfor> DownloadFile(string fileName, string folderType);
        IFormFile ShowFile(string fileName, string folderType);
        Task<string> UploadFile(IFormFile file, string fileName, string folderType);
    }
}