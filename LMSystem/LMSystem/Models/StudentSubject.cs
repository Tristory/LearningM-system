using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace LMSystem.Models
{
    public class StudentSubject
    {
        [Key]
        public int Id { get; set; }
        public bool IsMarked { get; set; }
        public DateTime AccessD {  get; set; }

        // Foreign Key
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
    }
}
