using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        // Foreign Connection
        public ICollection<ClassMaterial> ClassMaterials { get; set; }
        public ICollection<Lecture> Lectures { get; set; }

        // Foreign Key
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
    }
}
