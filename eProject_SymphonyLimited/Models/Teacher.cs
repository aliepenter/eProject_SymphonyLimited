using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Teacher
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Teacher Name")]
        [Required(ErrorMessage = "Please enter teacher's name!")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}