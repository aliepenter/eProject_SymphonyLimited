using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eProject_SymphonyLimited.Models.Authorize
{
    public class Permission
    {
        [Key]
        public int EntityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BusinessId { get; set; }

        [ForeignKey("BusinessId")]
        public Business Businesses { get; set; }
        public ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}