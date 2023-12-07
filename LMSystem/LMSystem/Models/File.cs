using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Type { get; set; }
        public DateTime LastChangedD { get; set; }

        // Foreign Connection
        public Exam Exam { get; set; }
        public Material Material { get; set; }

        // Foreign Key
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser ApplicationUser { get; set; }
    }

    public class FileInfor
    {
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public string? FilePath { get; set; }
    }
}
