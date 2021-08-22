using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class PaidRegisterViewModel
    {
        public int EntityId { get; set; }

        public string RollNumber { get; set; }

        public double Result { get; set; }

        public int RegisterInfoId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Status { get; set; }

        public DateTime EndTime { get; set; }

        public bool Tested { get; set; }

        public int AdmissionId { get; set; }

        public string Admission { get; set; }
    }
}