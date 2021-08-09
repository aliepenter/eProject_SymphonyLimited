using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Faq
    {
        [Key]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Please enter question!")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Please enter answer!")]
        public string Answer { get; set; }
    }
}