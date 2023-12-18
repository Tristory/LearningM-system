using LMSystem.Models;

namespace LMSystem.Repository
{
    public interface ISettingMService
    {
        Task<string> ChangePotraitSetting(Setting setting, IFormFile uploadFile);
        string CreateSetting(Setting setting);
        string DeletePortrait(Setting setting);
        string DeleteSetting(Setting setting);
        Setting GetSetting(int id);
        Setting GetUserSetting(string userId);
        string NotitfySetting(Setting setting);
        string UpdateSetting(Setting setting);
    }
}