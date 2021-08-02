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

        [Display(Name = "Question")]
        public string Question { get; set; }

        [Display(Name = "Answer")]
        public string Answer { get; set; }
    }
}