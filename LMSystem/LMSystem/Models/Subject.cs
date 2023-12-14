using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime SendingD { get; set; }

        // Foreign Connection
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
        public ICollection<Class> Classes { get; set; }

        // Foreign Key
        public string? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
