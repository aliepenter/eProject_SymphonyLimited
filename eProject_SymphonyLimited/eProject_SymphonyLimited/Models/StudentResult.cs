using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class StudentResult
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Mark")]
        [Required(ErrorMessage = "Please enter student's mark!")]
        public float Mark { get; set; }

        [Display(Name = "Max Mark")]
        [Required(ErrorMessage = "Please enter student's max mark!")]
        public float MaxMark { get; set; }

        [Display(Name = "Exam Times")]
        [Required(ErrorMessage = "Please enter student's exam times!")]
        public float ExamTimes { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public int MarkTypeId { get; set; }

        [ForeignKey("MarkTypeId")]
        public MarkType MarkType { get; set; }

        public int CommentId { get; set; }

        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }
    }
}