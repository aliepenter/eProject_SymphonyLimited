using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject_SymphonyLimited.Models
{
    public class RegisterInfo
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; }
        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]

        public Admission Admission { get; set; }
    }
}