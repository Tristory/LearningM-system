using LMSystem.Data;
using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Services
{
    public class SettingMService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileHandlerService _fileHandlerService;

        public SettingMService(ApplicationDbContext context, IFileHandlerService fileHandlerService)
        {
            _context = context;
            _fileHandlerService = fileHandlerService;
        }

        public Setting GetSetting(string userId)
        {
            return _context.Settings
                .Include(e => e.ApplicationUser)
                .FirstOrDefault(e => e.UserId == userId);
        }

        public string CreateSetting(Setting setting)
        {
            _context.Settings.Add(setting);
            _context.SaveChanges();

            return "Create success!";
        }

        public string UpdateSetting(Setting setting)
        {
            _context.Settings.Update(setting);
            _context.SaveChanges();

            return "Update success!";
        }

        public string DeleteSetting(Setting setting)
        {
            _context.Settings.Remove(setting);
            _context.SaveChanges();

            return "Delete success!";
        }

        public async Task<string> ChangePotraitSetting(Setting setting, IFormFile uploadFile)
        {
            var path = await _fileHandlerService.UploadFile(uploadFile, setting.UserId, "Portraits");

            setting.PotraitPic = path;

            return UpdateSetting(setting);
        }

        public string DeletePortrait(Setting setting)
        {
            var result = _fileHandlerService.DeleteFile(setting.UserId, "Portraits");

            setting.PotraitPic = string.Empty;

            return UpdateSetting(setting);
        }

        public string NotitfySetting(Setting setting)
        {
            setting.IsAcceptNotify = !setting.IsAcceptNotify;

            return UpdateSetting(setting);
        }
    }
}
