using Microsoft.AspNetCore.Identity;

namespace LMSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Foreign Connection
        public ICollection<File> Files { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public Setting Setting { get; set; }
    }

    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
