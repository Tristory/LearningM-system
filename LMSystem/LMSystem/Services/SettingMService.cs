using LMSystem.Data;
using LMSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Services
{
    public class SettingMService
    {
        private readonly ApplicationDbContext _context;

        public SettingMService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
