using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public bool IsAcceptNotify { get; set; }
        public string Description { get; set; }
        //public string Gender { get; set; }
        //public string Address { get; set; }
        public string PotraitPic { get; set; }

        // Foreign key
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }

    public class SettingFrame
    {
        public string Description { get; set; }
    }
}
