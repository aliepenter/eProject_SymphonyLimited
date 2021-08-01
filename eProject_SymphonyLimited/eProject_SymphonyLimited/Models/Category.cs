using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Category
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please enter category name!")]
        public string Name { get; set; }

        [Display(Name = "Level")]
        [Required(ErrorMessage = "Please choose level!")]
        public int Level { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please choose parent category!")]
        public int ParentId { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}