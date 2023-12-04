using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace LMSystem.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string status { get; set; }

    }
}
