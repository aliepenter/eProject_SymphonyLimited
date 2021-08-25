using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class ClassViewModel
    {
        public int EntityId { get; set; }
        public string Name { get; set; }
        public int QuantityStudent { get; set; }
        public int AdmissionId { get; set; }
        public string AdmissionName { get; set; }
    }
}