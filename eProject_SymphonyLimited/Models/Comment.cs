using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Comment
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Please enter comment!")]
        public string Name { get; set; }

        public ICollection<StudentResult> StudentResults { get; set; }
    }
}