using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using DotNetToolBox.Utils;
using System;
using System.IO;
using System.Security.Cryptography;

namespace DotNetToolBox.TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RSACryptoServiceProvider rsa = Cryptography.RSA.GenerateKeyPair(2048);

                byte[] data = RandomHelper.GenerateBytes(10);
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (FileStream fs = StreamHelper.GetFileStreamCreate(@"C:\Temp\file.enc"))
                    {
                        FileEnc.EncryptWithKey(ms, fs, rsa, "testkey01");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
