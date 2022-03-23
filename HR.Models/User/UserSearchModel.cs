using HR.Models.Common;
using System.Collections.Generic;

namespace HR.Models.User
{
    public class UserSearchModel : Paging
    {
        public string TextSearch { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
