using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class RegisterAdmission
    {
        [Key]
        public int EntityId { get; set; }

        public string RollNumber { get; set; }

        public double Mark { get; set; }

        public bool StatusInClass { get; set; }

        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]

        public Admission Admission { get; set; }
    }
}