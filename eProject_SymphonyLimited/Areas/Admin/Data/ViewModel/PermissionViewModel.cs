using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Areas.Admin.Data.ViewModel
{
    public class PermissionViewModel
    {
        public int EntityId { get; set; }
        public string BusinessId { get; set; }
        public string Description { get; set; }
        public bool IsGranted { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
    }
}