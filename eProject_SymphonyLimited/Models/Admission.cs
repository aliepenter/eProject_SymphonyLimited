using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter admission's bill time!")]
        public DateTime BillTime { get; set; }

        [Required(ErrorMessage = "Please enter admission's passed mark!")]
        public double PassedMark { get; set; }

        [Required(ErrorMessage = "Please enter admission's max mark!")]
        public double MaxMark { get; set; }

        [Required(ErrorMessage = "Please enter admission's price!")]
        public double Price { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]

        public Course Course { get; set; }
        public ICollection<Class> Classes { get; set; }
        public ICollection<RegisterInfo> RegisterInfoes { get; set; }
    }
}