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
        public static void Test()
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes("this is a secret message");
                byte[] dec;

                //Encrypt + decrypt with password
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (FileStream fs = new FileStream(@"C:\Temp\test1.enc", FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        DNTBEnc.EncryptWithPassword(ms, fs, "test1234");
                    }
                }

                using (FileStream fs = new FileStream(@"C:\Temp\test1.enc", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        DNTBEnc.DecryptWithPassword(fs, ms, "test1234");
                        dec = ms.ToArray();
                    }
                }

                string sdec = Encoding.Default.GetString(dec);

                //Encrypt + decrypt with RSA
                RSACryptoServiceProvider rsa = RSAEncryptor.GenerateKeyPair(4096);
                byte[] dec2;

                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (FileStream fs = new FileStream(@"C:\Temp\test2.enc", FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        DNTBEnc.EncryptWithRSA(ms, fs, rsa, "MyKeyName", DNTBEnc.KeyType.XML);
                    }
                }

                using (FileStream fs = new FileStream(@"C:\Temp\test2.enc", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        DNTBEnc.DecryptWithRSA(fs, ms, rsa);
                        dec2 = ms.ToArray();
                    }
                }

                string sdec2 = Encoding.Default.GetString(dec2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
