
using System.ComponentModel.DataAnnotations;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class UserViewModel
    {
        public int EntityId { get; set; }
        public string Account { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int GroupId { get; set; }
        public string  GroupUserName { get; set; }
    }
}