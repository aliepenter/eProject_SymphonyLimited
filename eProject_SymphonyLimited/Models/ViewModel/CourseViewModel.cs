using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models.ViewModel
{
    public class CourseViewModel
    {
        public int EntityId { get; set; }
        
        public string Name { get; set; }
        
        public string Time { get; set; }
        
        public string Description { get; set; }
        
        public float Price { get; set; }
        
        public string Subject { get; set; }
        
        public string Certificate { get; set; }
        
        public string Image { get; set; }
        
        public int CategoryId { get; set; }

        public string Category { get; set; }
    }
}