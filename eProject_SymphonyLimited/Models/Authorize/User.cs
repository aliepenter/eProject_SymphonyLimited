using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject_SymphonyLimited.Models.Authorize
{
    public class User
    {
        [Key]
        public int EntityId { get; set; }
        [Required(ErrorMessage = "Please enter account!")]
        public string Account { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter full name!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter phone!")]
        [RegularExpression("^(84|0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int GroupId { get; set; }

        [ForeignKey("GroupId")] // Chi ra khóa ngoại của liên kết
        public virtual GroupUser GroupUsers { get; set; }
    }
}