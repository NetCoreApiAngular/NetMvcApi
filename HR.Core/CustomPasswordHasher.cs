using HR.Core.Helpers;
using Microsoft.AspNetCore.Identity;

namespace HR.Core
{
    public class CustomPasswordHasher<TUser> : PasswordHasher<TUser> where TUser : class
    {
        public override string HashPassword(TUser user, string password)
        {
            return CryptographyHelper.HashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            var result = CryptographyHelper.ValidatePassword(providedPassword, hashedPassword);
            if(result)
                return PasswordVerificationResult.Success;
            return PasswordVerificationResult.Failed;

        }
    }
}