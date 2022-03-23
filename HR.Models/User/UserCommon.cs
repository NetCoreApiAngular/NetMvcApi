using System.Collections.Generic;

namespace HR.Models.User
{
    public class UserCommon
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Tel { get; set; }
        public string AddressLine1 { get; set; }
        public string[] Roles { get; set; }
        public bool IsAdmin { get; set; }
        public LoginResult Status { get; set; }
        public Dictionary<string, string> RoleModuleActions { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
    }
}
