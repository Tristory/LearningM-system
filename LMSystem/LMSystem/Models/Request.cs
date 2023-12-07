using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }

        // Foreign Key
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
