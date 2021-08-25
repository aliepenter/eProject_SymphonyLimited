using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class AdmsViewModel
    {
        public int EntityId { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime BillTime { get; set; }

        public double PassedMark { get; set; }

        public double MaxMark { get; set; }

        public double Price { get; set; }

        public int CourseId { get; set; }

        public string Course { get; set; }
        public string Image { get; set; }
    }
}