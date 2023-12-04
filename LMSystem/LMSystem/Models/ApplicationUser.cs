using Microsoft.AspNetCore.Identity;

namespace LMSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
    }

    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
