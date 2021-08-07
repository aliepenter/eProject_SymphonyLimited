using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Course
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Please enter course name!")]
        public string Name { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please enter course's time!")]
        public string Time { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please enter course's price!")]
        public float Price { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Please enter course's subject!")]
        public string Subject { get; set; }

        [Display(Name = "Certificate")]
        [Required(ErrorMessage = "Please enter course's certificate!")]
        public string Certificate { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]

        public Category Category { get; set; }
    }
}