using System.Security.Cryptography;

namespace DotNetToolBox.Cryptography
{
    public static class PBKDF2
    {
        /// <summary>
        /// Generate a pseudo-random key based on a password and salt
        /// </summary>
        /// <param name="nbBytes">Number of bytes to generate</param>
        /// <param name="password">Password</param>
        /// <param name="salt">Salt</param>
        /// <param name="iterations">Iterations</param>
        /// <returns></returns>
        public static byte[] GenerateKeyFromPassword(int nbBytes, string password, byte[] salt, int iterations = 10000)
        {
            using (Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pdb.GetBytes(nbBytes);
            }
        }
    }
}
