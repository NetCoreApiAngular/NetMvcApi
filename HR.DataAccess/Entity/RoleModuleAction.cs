using System;
using System.Collections.Generic;

namespace HR.DataAccess.Entity
{
    public partial class RoleModuleAction
    {
        public int RoleModuleActionId { get; set; }
        public int RoleId { get; set; }
        public int ModuleActionId { get; set; }

        public virtual ModuleAction ModuleAction { get; set; }
        public virtual Roles Role { get; set; }
    }
}
