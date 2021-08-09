using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class EavEntity
    {
        [Key]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Please enter entity name!")]
        public string Name { get; set; }

        public string Code { get; set; }

    }
}