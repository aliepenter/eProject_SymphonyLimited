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

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Please enter category name!")]
        public string Name { get; set; }

        [Display(Name = "Path")]
        public string Path { get; set; }

        [Display(Name = "Level")]
        public int Level { get; set; }

        [Display(Name = "Parent")]
        [Required(ErrorMessage = "Please choose parent category!")]
        public int ParentId { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}