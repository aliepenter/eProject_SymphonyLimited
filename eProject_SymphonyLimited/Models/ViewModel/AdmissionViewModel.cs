using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models.ViewModel
{
    public class AdmissionViewModel
    {
        public int EntityId { get; set; }
        
        public string Name { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public int QuantityStudent { get; set; }
        
        public double Price { get; set; }

        public double MarkPass { get; set; }

        public int CourseId { get; set; }

        public string Course { get; set; }
    }
}