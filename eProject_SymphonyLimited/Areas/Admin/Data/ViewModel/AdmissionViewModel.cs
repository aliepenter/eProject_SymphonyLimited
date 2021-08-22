using System;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class AdmissionViewModel
    {
        public int EntityId { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime BillTime { get; set; }

        public int QuantityStudent { get; set; }

        public double Price { get; set; }

        public int CourseId { get; set; }

        public string Course { get; set; }
    }
}