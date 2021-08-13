using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models.Authorize
{
    public class Business
    {
        [Key]
        public string EntityId { get; set; }
        public string Name { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}