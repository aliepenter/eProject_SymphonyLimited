using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models.Authorize
{
    public class User
    {
        [Key]
        public int EntityId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int GroupId { get; set; }


        [ForeignKey("GroupId")] // Chi ra khóa ngoại của liên kết
        public GroupUser GroupUsers { get; set; }
    }
}