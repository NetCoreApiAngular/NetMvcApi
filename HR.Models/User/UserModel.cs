using System;

namespace HR.Models.User
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsSupperAdmin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public bool IsLockedOut { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }
        public bool Remember { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Tel { get; set; }
        public string Thumbnail { get; set; }
        public bool IsChangePassword { get; set; }
        public string Description { get; set; }
    }
}
