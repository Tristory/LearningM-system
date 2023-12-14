using LMSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace LMSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        // Set Up Db
        public DbSet<Models.File> Files { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassMaterial> ClassMaterials { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
    }
}
