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
                byte[] hash = MD5.Hash(data);
                string sHash = Hex.Encode(hash);
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
                byte[] hash = SHA1.Hash(data);
                string sHash = Hex.Encode(hash);
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
                byte[] hash = SHA256.Hash(data);
                string sHash = Hex.Encode(hash);
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
                byte[] hash = SHA512.Hash(data);
                string sHash = Hex.Encode(hash);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
