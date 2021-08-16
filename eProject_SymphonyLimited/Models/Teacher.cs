using System.ComponentModel.DataAnnotations;

namespace eProject_SymphonyLimited.Models
{
    public class Teacher
    {
        [Key]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Please enter Name!")]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Please enter Specialize!")]
        public string Specialize { get; set; }
        [Required(ErrorMessage = "Please enter Subject!")]
        public string Subject { get; set; }
    }
}