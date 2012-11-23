using System;
using System.Security.Cryptography;
using System.Text;

namespace Application.Domain.Domains
{
    public class User : BaseDomainEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public static HashAlgorithm CurrentHashAlgorithm
        {
            get { return new SHA256Managed(); }
        }

        public static string GenerateSalt()
        {
            var random = new Random();
            var saltSize = random.Next(5, 9);
            var saltBytes = new byte[saltSize];

            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public static string GenerateSaltedHash(string password, string salt)
        {
            byte[] passwordByte = Encoding.UTF8.GetBytes(password);
            byte[] saltByte = Convert.FromBase64String(salt);

            var algorithm = CurrentHashAlgorithm;

            var plainTextWithSaltBytes = new byte[passwordByte.Length + saltByte.Length];

            for (var i = 0; i < passwordByte.Length; i++)
            {
                plainTextWithSaltBytes[i] = passwordByte[i];
            }

            for (var i = 0; i < saltByte.Length; i++)
            {
                plainTextWithSaltBytes[passwordByte.Length + 1] = saltByte[i];
            }

            var hash = algorithm.ComputeHash(plainTextWithSaltBytes);

            return Convert.ToBase64String(hash);
        }

        #region Equality members

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.UserName, UserName) && Equals(other.Password, Password);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (User)) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (UserName != null ? UserName.GetHashCode() : 0);
                result = (result*397) ^ (Password != null ? Password.GetHashCode() : 0);
                return result;
            }
        }

        #endregion
    }
}