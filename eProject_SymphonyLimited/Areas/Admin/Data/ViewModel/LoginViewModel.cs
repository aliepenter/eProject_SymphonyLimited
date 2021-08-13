using System.ComponentModel.DataAnnotations;

namespace eProject_SymphonyLimited.Areas.Admin.Data.Model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter account!")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        public string Password { get; set; }
    }
}