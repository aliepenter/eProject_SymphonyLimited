using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Admission
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Admission Name")]
        [Required(ErrorMessage = "Please enter admission's name!")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter admission's start time!")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter admission's end time!")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Please enter admission's quantity student!")]
        public int QuantityStudent { get; set; }

        [Required(ErrorMessage = "Please enter admission's price!")]
        public double Price { get; set; }

        public double MarkPass { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]

        public Course Course { get; set; }

        public ICollection<Class> Classes { get; set; }
        public ICollection<RegisterInfo> RegisterInfoes { get; set; }
    }
}