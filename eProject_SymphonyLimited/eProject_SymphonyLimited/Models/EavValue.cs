using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class EavValue
    {
        [Key]
        public int EntityId { get; set; }

        public int AttributeId { get; set; }

        public string Value { get; set; }
    }
}