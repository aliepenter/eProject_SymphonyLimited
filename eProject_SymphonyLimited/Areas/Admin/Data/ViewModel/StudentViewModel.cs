using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class StudentViewModel
    {
        public int EntityId { get; set; }

        public string StudentName { get; set; }

        public bool Status { get; set; }

        public int RegisterInfoId { get; set; }

        public int ClassId { get; set; }

        public int AdmissionId { get; set; }
    }
}