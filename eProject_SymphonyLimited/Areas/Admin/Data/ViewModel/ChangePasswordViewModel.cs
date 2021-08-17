using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword),ErrorMessage = "Confirm Password doesn't match.")]
        public string ConfirmPassword { get; set; }
    }
}