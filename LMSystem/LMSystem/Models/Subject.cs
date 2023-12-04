using System.ComponentModel.DataAnnotations;

namespace LMSystem.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
