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

        [Display(Name = "Branch Name")]
        [Required(ErrorMessage = "Please enter branch's name!")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter branch's description!")]
        public string Description { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter branch's address!")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter branch's email!")]
        public string Email { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Time")]
        public string Time { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Please enter branch's phone!")]
        public string Phone { get; set; }
    }
}