using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Class
    {
        [Key]
        public int EntityId { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}