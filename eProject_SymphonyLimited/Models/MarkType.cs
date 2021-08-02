using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class MarkType
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Mark Type Name")]
        [Required(ErrorMessage = "Please enter mark type name!")]
        public string Name { get; set; }

        public ICollection<StudentResult> StudentResults { get; set; }
    }
}