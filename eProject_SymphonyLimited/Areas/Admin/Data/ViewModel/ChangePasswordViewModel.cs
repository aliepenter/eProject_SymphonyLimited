using System.ComponentModel.DataAnnotations;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class ChangePasswordViewModel
    {
        public int EntityId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't match.")]
        public string ConfirmPassword { get; set; }
    }
}