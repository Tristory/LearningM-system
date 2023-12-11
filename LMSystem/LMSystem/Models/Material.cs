using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace LMSystem.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        public string status { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ValidatedD {  get; set; }

        // Foreign Key
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public File File { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

    }
}
