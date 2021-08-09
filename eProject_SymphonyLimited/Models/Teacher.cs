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

        [Required(ErrorMessage = "Please enter name!")]
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}