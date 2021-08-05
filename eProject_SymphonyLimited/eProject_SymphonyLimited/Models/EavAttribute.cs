using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class EavAttribute
    {
        [Key]
        public int EntityId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int EavEntityId { get; set; }
    }
}