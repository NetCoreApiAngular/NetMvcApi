using System;
using System.Collections.Generic;

namespace HR.DataAccess.Entity
{
    public partial class ModuleAction
    {
        public ModuleAction()
        {
            RoleModuleAction = new HashSet<RoleModuleAction>();
        }

        public int ModuleActionId { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public int? OrderIndex { get; set; }
        public int? Group { get; set; }

        public virtual ICollection<RoleModuleAction> RoleModuleAction { get; set; }
    }
}
