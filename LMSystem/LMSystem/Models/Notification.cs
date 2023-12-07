using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsStored { get; set; }
        public DateTime ReceivedD { get; set; }

        // Foreign Key
        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
