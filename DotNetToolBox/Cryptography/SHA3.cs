using Org.BouncyCastle.Crypto.Digests;
using System.IO;

namespace DotNetToolBox.Cryptography
{
    public static class SHA3
    {
        /// <summary>
        /// Compute the MD5 hash value
        /// </summary>
        /// <param name="input">Input Stream</param>
        public static byte[] Hash(Stream input, int bitLength = 512)
        {
            Sha3Digest sha3 = new Sha3Digest(bitLength);
            int bytesRead;
            byte[] buffer = new byte[4096];

            do
            {
                bytesRead = input.Read(buffer, 0, 4096);
                if (bytesRead > 0)
                    sha3.BlockUpdate(buffer, 0, bytesRead);
            } while (bytesRead == 4096);

            byte[] result = new byte[bitLength / 8];
            sha3.DoFinal(result, 0);

            return result;
        }
    }
}
