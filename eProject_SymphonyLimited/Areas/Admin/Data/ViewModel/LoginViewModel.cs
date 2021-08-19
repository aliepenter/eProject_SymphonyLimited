using System.ComponentModel.DataAnnotations;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter account!")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}