﻿using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LMSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterUser(LoginUser user)
        {
            var appUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.UserName,
            };

            var result = await _userManager.CreateAsync(appUser, user.Password);

            return result.Succeeded;
        }

        public async Task<string> Login(LoginUser user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.UserName);
            if (appUser == null)
                return "Can't find user";

            var result = await _userManager.CheckPasswordAsync(appUser, user.Password);

            if (!result)
                return "The Password is wrong";

            return await CreateToken(appUser);
        }

        public bool Logout()
        {
            // Sign out the token

            return true;
        }

        private async Task<string> CreateToken(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
                //new Claim(ClaimTypes.Role, role)
            };

            var roles = await _userManager.GetRolesAsync(user);
            var claimRoles = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            claims.AddRange(claimRoles);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("The super duper secret key that should be long!"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<bool> CreateRole(string roleName)
        {
            var role = new IdentityRole
            {
                Name = roleName,
            };

            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded;
        }

        public async Task<string> GiveRole(string userName, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            if (user == null)
                return "There is no account like this!";

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return "There is no role like that!";

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return "Add success!";
        }
    }
}