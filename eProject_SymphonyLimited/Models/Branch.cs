using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Branch
    {
        [Key]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Please enter name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter email!")]
        public string Email { get; set; }

        public string Image { get; set; }

        public string Time { get; set; }
        [Required(ErrorMessage = "Please enter phone!")]
        [RegularExpression("^(84|0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter address!")]
        public string Address { get; set; }
    }
}