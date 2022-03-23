using System;
using System.Collections.Generic;

namespace HR.DataAccess.Entity
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Roles Role { get; set; }
        public virtual User User { get; set; }
    }
}
