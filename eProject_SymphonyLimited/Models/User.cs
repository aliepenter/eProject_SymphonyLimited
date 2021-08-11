using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class User
    {
        [Key]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Please enter account!")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}