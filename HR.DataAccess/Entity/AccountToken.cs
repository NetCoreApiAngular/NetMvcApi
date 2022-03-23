using System;
using System.Collections.Generic;

namespace HR.DataAccess.Entity
{
    public partial class AccountToken
    {
        public int AccountTokenId { get; set; }
        public int AccountId { get; set; }
        public bool? IsAdminAccountSide { get; set; }
        public string TokenKey { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int TokenType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
