using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Repository
{
    public interface IAuthService
    {
        Task<bool> CreateRole(string roleName);
        Task<string> GiveRole(string userName, string roleName);
        Task<string> Login(LoginUser user);
        bool Logout();
        Task<bool> RegisterUser(LoginUser user);
    }
}