using LMSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Repository
{
    public interface IAuthService
    {
        Task<string> ChangePassword(ApplicationUser user, string currentPassword, string newPassword);
        Task<bool> CreateRole(string roleName);
        Task<bool> DeleteRole(string roleName);
        Task<bool> DeleteUser(string userId);
        Task<string> DeleteUserRole(string userName, string roleName);
        Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);
        Task<ApplicationUser> GetApplicationUserByNameAsync(string userName);
        Task<List<ApplicationUser>> GetApplicationUsers();
        Task<List<IdentityRole>> GetRoles();
        Task<List<ApplicationUser>> GetUserByNameOrId(string searchS);
        Task<List<ApplicationUser>> GetUsersByRole(string roleName);
        Task<string> GiveUserRole(string userName, string roleName);
        Task<string> Login(LoginUser user);
        bool Logout();
        Task<bool> RegisterUser(LoginUser user);
    }
}