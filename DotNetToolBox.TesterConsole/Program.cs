using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
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
                //RSACryptoServiceProvider rsa = Cryptography.RSA.GenerateKeyPair();
                //using (FileStream fs = StreamHelper.GetFileStreamCreate(@"C:\Temp\pub.pem"))
                //{
                //    Cryptography.RSA.SavePublicKeyToPEM(rsa, fs);
                //}
                //using (FileStream fs = StreamHelper.GetFileStreamCreate(@"C:\Temp\pk.pem"))
                //{
                //    Cryptography.RSA.SavePrivateKeyToPEM(rsa, fs, "test1234");
                //}


                //string path = @"C:\Temp\fileenc\clear";

                //for (int i = 0; i < 10; i++)
                //{
                //    string file = Path.Combine(path, $"file{i}.bin");

                //    byte[] data = RandomHelper.GenerateBytes(i * 1024);
                //    using (MemoryStream ms = new MemoryStream(data))
                //    {
                //        using (FileStream fs = StreamHelper.GetFileStreamCreate(file))
                //        {
                //            StreamHelper.WriteStream(ms, fs);
                //        }
                //    }
                //}


                RSACryptoServiceProvider rsa;
                using (FileStream fs = StreamHelper.GetFileStreamOpen(@"C:\Temp\pub.pem"))
                {
                    rsa = Cryptography.RSA.LoadFromPEM(fs);
                }

                string path = @"C:\Temp\fileenc\clear";
                string encPath = @"C:\Temp\fileenc\enc";
                string[] files = Directory.GetFiles(path);

                foreach(string file in files)
                {
                    string encFile = Path.Combine(encPath, Path.GetFileName(file) + ".enc");
                    using (FileStream fsIn = StreamHelper.GetFileStreamOpen(file))
                    {
                        using (FileStream fsOut = StreamHelper.GetFileStreamCreate(encFile))
                        {
                            FileEnc.EncryptWithKey(fsIn, fsOut, rsa, "testkey01");
                        }
                    }
                }

                //byte[] data = RandomHelper.GenerateBytes(10);
                //using (MemoryStream ms = new MemoryStream(data))
                //{
                //    using (FileStream fs = StreamHelper.GetFileStreamCreate(@"C:\Temp\file.enc"))
                //    {
                //        FileEnc.EncryptWithKey(ms, fs, rsa, "testkey01");
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        
    }
}
