using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Class
    {
        [Key]
        public int EntityId { get; set; }

        public string Name { get; set; }

        public int QuantityStudent { get; set; }

        public bool Status { get; set; }

        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]

        public virtual Admission Admission { get; set; }
    }
}