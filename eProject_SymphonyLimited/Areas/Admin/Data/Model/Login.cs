using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Areas.Admin.Data.Model
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter account!")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        public string Password { get; set; }
    }
}