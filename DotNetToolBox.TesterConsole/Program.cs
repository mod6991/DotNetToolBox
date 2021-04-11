using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using DotNetToolBox.Utils;
using System;
using System.Collections.Generic;
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
                Console.WriteLine("Generating key files...");
                GenerateKeyFiles();
                Console.WriteLine("Generating pass file...");
                GeneratePassFile();
                Console.WriteLine("Checking key files...");
                CheckKeyFiles();
                Console.WriteLine("Checking pass file...");
                CheckPassFile();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #region Read

        static void CheckKeyFiles()
        {
            CheckKeyFile(2048);
            CheckKeyFile(4096);
            CheckKeyFile(8192);
        }

        static void CheckKeyFile(int keySize)
        {
            string keyFile = $"C:\\Temp\\CS_{keySize}.tlv";

            using (FileStream fs = StreamHelper.GetFileStreamOpen(keyFile))
            {
                BinaryTlvReader reader = new BinaryTlvReader(fs);
                Dictionary<string, byte[]> values = reader.ReadAll();

                RSACryptoServiceProvider key;
                using (MemoryStream ms = new MemoryStream(values["Key"]))
                {
                    key = Cryptography.RSA.LoadFromPEM(ms, Hex.Encode(values["Pass"]));
                }

                Dictionary<string, byte[]> records = BinaryTlvReader.TlvListFromBytes(values["Recs"]);

                foreach(KeyValuePair<string, byte[]> kvp in records)
                {
                    Dictionary<string, byte[]> record = BinaryTlvReader.TlvListFromBytes(kvp.Value);

                    string sOriginalHash = Hex.Encode(record["Hash"]);

                    byte[] dec;
                    using(MemoryStream msIn = new MemoryStream(record["Enc"]))
                    {
                        using (MemoryStream msOut = new MemoryStream())
                        {
                            FileEnc.DecryptWithKey(msIn, msOut, key);
                            dec = msOut.ToArray();
                        }
                    }

                    byte[] hash;
                    using (MemoryStream ms = new MemoryStream(dec))
                        hash = SHA3.Hash(ms);

                    string sHash = Hex.Encode(hash);

                    if (sOriginalHash != sHash)
                    {
                        Console.WriteLine($"{keySize} {kvp.Key}: ERROR");
                    }
                    else
                        Console.WriteLine($"{keySize} {kvp.Key}: OK");
                }
            }
        }

        static void CheckPassFile()
        {
            string passFile = $"C:\\Temp\\CS_PASS.tlv";

            using (FileStream fs = StreamHelper.GetFileStreamOpen(passFile))
            {
                BinaryTlvReader reader = new BinaryTlvReader(fs);
                Dictionary<string, byte[]> values = reader.ReadAll();

                foreach(KeyValuePair<string, byte[]> kvp in values)
                {
                    Dictionary<string, byte[]> record = BinaryTlvReader.TlvListFromBytes(kvp.Value);

                    string sOriginalHash = Hex.Encode(record["Hash"]);
                    string password = Hex.Encode(record["Pass"]);

                    byte[] dec;
                    using (MemoryStream msIn = new MemoryStream(record["Enc"]))
                    {
                        using (MemoryStream msOut = new MemoryStream())
                        {
                            FileEnc.DecryptWithPassword(msIn, msOut, password);
                            dec = msOut.ToArray();
                        }
                    }

                    byte[] hash;
                    using (MemoryStream ms = new MemoryStream(dec))
                        hash = SHA3.Hash(ms);

                    string sHash = Hex.Encode(hash);

                    if (sOriginalHash != sHash)
                    {
                        Console.WriteLine($"{kvp.Key}: ERROR");
                    }
                    else
                        Console.WriteLine($"{kvp.Key}: OK");
                }
            }
        }

        #endregion

        #region Generate

        static void GenerateKeyFiles()
        {
            GenerateKeyFile(2048);
            GenerateKeyFile(4096);
            GenerateKeyFile(8192);
        }

        static void GenerateKeyFile(int keySize)
        {
            string keyFile = $"C:\\Temp\\CS_{keySize}.tlv";

            RSACryptoServiceProvider key = Cryptography.RSA.GenerateKeyPair(keySize);

            byte[] passwordData = RandomHelper.GenerateBytes(8);

            byte[] privateKeyData;
            using (MemoryStream ms = new MemoryStream())
            {
                Cryptography.RSA.SavePrivateKeyToPEM(key, ms, Hex.Encode(passwordData));
                privateKeyData = ms.ToArray();
            }

            using (FileStream fs = StreamHelper.GetFileStreamCreate(keyFile))
            {
                BinaryTlvWriter writer = new BinaryTlvWriter(fs, 4);
                writer.Write("Key", privateKeyData);
                writer.Write("Pass", passwordData);

                byte[] records;
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryTlvWriter writer2 = new BinaryTlvWriter(ms, 3);

                    for (int i = 0; i < 100; i++)
                    {
                        byte[] data = RandomHelper.GenerateBytes(i * 1024);
                        byte[] hash;
                        using (MemoryStream hashMsIn = new MemoryStream(data))
                            hash = SHA3.Hash(hashMsIn);

                        byte[] enc;
                        using (MemoryStream msIn = new MemoryStream(data))
                        {
                            using (MemoryStream msOut = new MemoryStream())
                            {
                                FileEnc.EncryptWithKey(msIn, msOut, key, $"CS{keySize}PK");
                                enc = msOut.ToArray();
                            }
                        }

                        byte[] recordData = BinaryTlvWriter.BuildTlvList(new Dictionary<string, byte[]>()
                        {
                            { "Hash", hash },
                            { "Enc", enc }
                        }, 4);

                        writer2.Write($"R{i:00}", recordData);
                    }

                    records = ms.ToArray();
                }

                writer.Write("Recs", records);
            }
        }

        static void GeneratePassFile()
        {
            string passFile = $"C:\\Temp\\CS_PASS.tlv";

            using (FileStream fs = StreamHelper.GetFileStreamCreate(passFile))
            {
                BinaryTlvWriter writer = new BinaryTlvWriter(fs, 3);

                for (int i = 0; i < 100; i++)
                {
                    byte[] passwordData = RandomHelper.GenerateBytes(8);
                    byte[] data = RandomHelper.GenerateBytes(i * 1024);

                    byte[] hash;
                    using (MemoryStream ms = new MemoryStream(data))
                        hash = SHA3.Hash(ms);

                    byte[] enc;
                    using (MemoryStream msIn = new MemoryStream(data))
                    {
                        using (MemoryStream msOut = new MemoryStream())
                        {
                            FileEnc.EncryptWithPassword(msIn, msOut, Hex.Encode(passwordData));
                            enc = msOut.ToArray();
                        }
                    }

                    byte[] recData = BinaryTlvWriter.BuildTlvList(new Dictionary<string, byte[]>()
                    {
                        { "Pass", passwordData },
                        { "Hash", hash },
                        { "Enc", enc },
                    }, 4);

                    writer.Write($"R{i:00}", recData);
                }
            }
        }

        #endregion
    }
}
