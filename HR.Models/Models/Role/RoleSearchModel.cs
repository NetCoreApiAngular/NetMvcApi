using HR.Models.Common;
using System.Collections.Generic;

namespace HR.Models.Role
{
    public class RoleSearchModel : Paging
    {
        public string TextSearch { get; set; }

        public List<RoleModel> Roles { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
}
