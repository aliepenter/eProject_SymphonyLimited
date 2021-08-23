using System;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class AdmissionViewModel
    {
        public int EntityId { get; set; }

        public string Name { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string BillTime { get; set; }

        public int QuantityStudent { get; set; }

        public double Price { get; set; }

        public int CourseId { get; set; }

        public string Course { get; set; }
        public string Image { get; set; }
    }
}