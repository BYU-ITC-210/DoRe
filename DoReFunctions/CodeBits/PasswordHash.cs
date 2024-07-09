using System;
using System.Security.Cryptography;

namespace Bredd.CodeBit
{

    /// <summary>
    /// This is a thin wrapper around Rfc2898DeriveBytes that provides simple password
    /// hashing and validation services.
    /// </summary>
    /// <remarks>
    /// Typically you would hash a password before storing it in a database or other persistent storage.
    /// </remarks>
    internal class PasswordHash
    {
        const int c_saltBytes = 16;
        const int c_hashIterations = 100000;
        const int c_hashBytes = 32;
        static readonly HashAlgorithmName c_hashAlgorithm = HashAlgorithmName.SHA256;

        /// <summary>
        /// Hash a password for storage in a database
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>The hashed password.</returns>
        /// <remarks>
        /// <para>This is a helper function not related to authentication tokens.</para>
        /// </remarks>
        public static string Hash(string password)
        {
            return HashPassword(RandomNumberGenerator.GetBytes(c_saltBytes), password);
        }

        /// <summary>
        /// Test whether a password matches a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>True if the password matches the hash.</returns>
        public static bool Validate(string password, string hash)
        {
            try
            {
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash)) return false;

                int colon = hash.IndexOf(':');
                if (colon < 0) return false;

                byte[] salt = Convert.FromHexString(hash.Substring(0, colon));

                string newHash = HashPassword(salt, password);

                return string.Equals(hash, newHash, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private static string HashPassword(byte[] salt, string password)
        {
            using (var hash = new Rfc2898DeriveBytes(password, salt, c_hashIterations, c_hashAlgorithm))
            {
                return string.Concat(Convert.ToHexString(salt), ":", Convert.ToHexString(hash.GetBytes(c_hashBytes)));
            }
        }

    }
}
