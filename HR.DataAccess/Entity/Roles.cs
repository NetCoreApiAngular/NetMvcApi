using System;
using System.Collections.Generic;

namespace HR.DataAccess.Entity
{
    public partial class Roles
    {
        public Roles()
        {
            RoleModuleAction = new HashSet<RoleModuleAction>();
            UserRole = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public int? Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedByUserId { get; set; }
        public string UpdatedUserId { get; set; }

        public virtual ICollection<RoleModuleAction> RoleModuleAction { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
