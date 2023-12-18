using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string Note { get; set; }
        public string Format {  get; set; }
        public int Duration {  get; set; }
        public string status { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ValidatedD { get; set; }

        // Foreign Key
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public File File { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

    }

    public class ExamFrame
    {
        public string Note { get; set; }
        public string Format { get; set; }
        public int Duration { get; set; }
        //public string status { get; set; }
        //public bool IsApproved { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime ValidatedD { get; set; }
        //public int FileId { get; set; }
        public int SubjectId { get; set; }
    }
}
