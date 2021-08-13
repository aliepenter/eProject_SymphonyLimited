using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models.Authorize
{
    public class GroupUser
    {
        [Key]
        public int EntityId { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}