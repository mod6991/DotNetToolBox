using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using System;
using System.Text;

namespace DotNetToolBox.Tester
{
    public static class HashTest
    {
        public static void Md5()
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes("Data to hash");
                byte[] hash = MD5Hasher.Hash(data);
                string sHash = HexadecimalEncoder.Encode(hash);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Sha1()
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes("Data to hash");
                byte[] hash = SHA1Hasher.Hash(data);
                string sHash = HexadecimalEncoder.Encode(hash);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Sha256()
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes("Data to hash");
                byte[] hash = SHA256Hasher.Hash(data);
                string sHash = HexadecimalEncoder.Encode(hash);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Sha512()
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes("Data to hash");
                byte[] hash = SHA512Hasher.Hash(data);
                string sHash = HexadecimalEncoder.Encode(hash);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
