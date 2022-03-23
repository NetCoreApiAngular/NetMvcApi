using System;
using System.Collections.Generic;

namespace HR.Models.User
{
    public class UserInfoModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }
        public string Tel { get; set; }
    }
}
