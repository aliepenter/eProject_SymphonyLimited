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
        [Required(ErrorMessage = "Please enter course's description!")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please enter course's price!")]
        public float Price { get; set; }

        [Display(Name = "Object")]
        [Required(ErrorMessage = "Please enter course's object!")]
        public string Object { get; set; }

        [Display(Name = "Certificate")]
        [Required(ErrorMessage = "Please enter course's certificate!")]
        public string Certificate { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]

        public Category Category { get; set; }

        public ICollection<StudentResult> StudentResults { get; set; }

        public ICollection<RegisterInfo> RegisterInfos { get; set; }
    }
}