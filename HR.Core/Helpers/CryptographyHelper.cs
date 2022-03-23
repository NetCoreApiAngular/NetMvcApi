using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace HR.Core.Helpers
{
    public static class CryptographyHelper
    {
        #region Const

        private const int SaltBytes = 24;
        private const int HashBytes = 24;
        private const int Pbkdf2Iterations = 1000;

        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int Rfc2898Index = 2;

        #endregion
        public static string CreateRandomPassword(int length = 7)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static string HashPassword(string password)
        {
            // random khóa 
            using (var rngCryp = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltBytes];
                rngCryp.GetBytes(salt);

                // Hash the password and encode the parameters
                byte[] hash = Rfc2898Deriver(password, salt, Pbkdf2Iterations, HashBytes);

                return Pbkdf2Iterations + ":" + Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        public static bool ValidatePassword(string password, string goodHash)
        {
            if (string.IsNullOrWhiteSpace(goodHash))
            {
                throw new ArgumentNullException("goodHash");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = goodHash.Split(delimiter);

            int iterations;
            if (int.TryParse(split[IterationIndex], out iterations))
            {
                iterations = Int32.Parse(split[IterationIndex], CultureInfo.InvariantCulture);
            }
            else
            {
                return false;
            }
            
            byte[] salt = Convert.FromBase64String(split[SaltIndex]);
            byte[] hash = Convert.FromBase64String(split[Rfc2898Index]);

            byte[] testHash = Rfc2898Deriver(password, salt, iterations, hash.Length);

            if (hash.Length != testHash.Length) return false;
            return !hash.Where((t, i) => t != testHash[i]).Any();
        }

        private static byte[] Rfc2898Deriver(string password, byte[] salt, int iterations, int outputMaxByte)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt))
            {
                deriveBytes.IterationCount = iterations;
                return deriveBytes.GetBytes(outputMaxByte);
            }
        }

        public static string Base64Encode(string plainText)
        {
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception)
            {

                return string.Empty;
            }
           
        }

        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception)
            {

                return string.Empty;
            }
        
        }

    }
}