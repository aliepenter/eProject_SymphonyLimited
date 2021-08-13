using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models.Authorize
{
    public class GroupPermission
    {
        [Column(Order = 0), Key]
        public int GroupId { get; set; }
        [Column(Order = 1), Key]
        public int PermissionId { get; set; }

        [ForeignKey("GroupId")]
        public GroupUser GroupUsers { get; set; }
        [ForeignKey("PermissionId")]
        public Permission Permissions { get; set; }
    }
}