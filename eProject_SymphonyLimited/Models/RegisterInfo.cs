using System.ComponentModel.DataAnnotations;

namespace eProject_SymphonyLimited.Models
{
    public class RegisterInfo
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}