using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Student
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Please enter student's name!")]
        public string Name { get; set; }

        [Display(Name = "Roll Number")]
        [Required(ErrorMessage = "Please enter student's roll number!")]
        public string RollNumber { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter student's address!")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter student's email!")]
        public string Email { get; set; }
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public Class Class { get; set; }
        public ICollection<StudentResult> StudentResults { get; set; }
    }
}