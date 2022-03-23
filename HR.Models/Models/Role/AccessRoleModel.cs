using HR.Models.Role;
using System.Collections.Generic;

namespace HR.Models
{
    public class AccessRoleModel
    {
        public List<RoleModel> Roles { get; set; }
        public List<ModuleActionModel> ModuleActions { get; set; }
        public int RoleId { get; set; }
    }
}
