using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUser user)
        {
            if (user == null)
                return Content("No user Input");

            if (await _authService.RegisterUser(user))
                return Ok("Finish making new account");

            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (user == null)
                return Content("No user Input");

            var result = await _authService.Login(user);

            return Content(result);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            if (_authService.Logout())
                return Ok();

            return BadRequest();
        }

        [HttpPost("Add role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (roleName == null)
                return Content("No Role Name Inputed!");

            if (await _authService.CreateRole(roleName))
                return Ok("Great work!");

            return BadRequest();
        }

        [HttpPost("Give role")]
        public async Task<IActionResult> GiveRole(string userName, string roleName)
        {
            if (userName == null || roleName == null)
                return Content("Please input correctly");

            var result = await _authService.GiveUserRole(userName, roleName);

            return Content(result);
        }        
    }
}
