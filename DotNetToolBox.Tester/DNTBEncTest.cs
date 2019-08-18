using DotNetToolBox.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DotNetToolBox.Tester
{
    public static class DNTBEncTest
    {
        private static int _total;

        public static void Test()
        {
            try
            {
                RSACryptoServiceProvider rsa = RSAEncryptor.GenerateKeyPair(4096);
                string test = rsa.ToXmlString(true);
                byte[] data = Encoding.Default.GetBytes("this is a secret message");
                byte[] dec;

                _total = 0;
                Console.WriteLine("EncryptWithPassword");
                //Encrypt + decrypt with password
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (FileStream fs = new FileStream(@"C:\Temp\test1.enc", FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        DNTBEnc.EncryptWithPassword(ms, fs, "test1234", DNTBEnc.Algorithm.AES, 4096, NotifyProgression);
                    }
                }
                Console.WriteLine("Total: {0}", _total);

                _total = 0;
                Console.WriteLine("DecryptWithPassword");
                using (FileStream fs = new FileStream(@"C:\Temp\test1.enc", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        DNTBEnc.DecryptWithPassword(fs, ms, "test1234", 4096, NotifyProgression);
                        dec = ms.ToArray();
                    }
                }
                Console.WriteLine("Total: {0}", _total);

                string sdec = Encoding.Default.GetString(dec);

                //Encrypt + decrypt with RSA
                byte[] dec2;

                _total = 0;
                Console.WriteLine("EncryptWithRSA");
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (FileStream fs = new FileStream(@"C:\Temp\test2.enc", FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        DNTBEnc.EncryptWithRSA(ms, fs, rsa, "MyKeyName", DNTBEnc.KeyType.XML, DNTBEnc.Algorithm.AES, 4096, NotifyProgression);
                    }
                }
                Console.WriteLine("Total: {0}", _total);

                _total = 0;
                Console.WriteLine("EncryptWithRSA");
                using (FileStream fs = new FileStream(@"C:\Temp\test2.enc", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        DNTBEnc.DecryptWithRSA(fs, ms, rsa, 4096, NotifyProgression);
                        dec2 = ms.ToArray();
                    }
                }
                Console.WriteLine("Total: {0}", _total);

                string sdec2 = Encoding.Default.GetString(dec2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void NotifyProgression(int bytesRead)
        {
            _total += bytesRead;
            Console.WriteLine(bytesRead);
        }
    }
}
