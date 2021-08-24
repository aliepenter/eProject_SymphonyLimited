using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models
{
    public class Student
    {
        [Key]
        public int EntityId { get; set; }

        public int RegisterInfoId { get; set; }

        public bool Status { get; set; }

        public int ClassId { get; set; }
    }
}