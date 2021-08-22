using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class PaidRegister
    {
        [Key]
        public int EntityId { get; set; }

        [Display(Name = "Roll Number")]
        public string RollNumber { get; set; }

        [Display(Name = "Result")]
        public double Result { get; set; }

        [Display(Name = "Tested?")]
        public bool Tested { get; set; }

        public int RegisterInfoId { get; set; }
    }
}