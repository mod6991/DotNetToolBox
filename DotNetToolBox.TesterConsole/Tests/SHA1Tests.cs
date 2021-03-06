﻿using DotNetToolBox.IO;
using System.IO;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class SHA1Tests
    {
        internal static int Hash(string file)
        {
            int i = 0;

            using (FileStream fs = StreamHelper.GetFileStreamOpen(file))
            {
                byte[] rec;
                do
                {
                    rec = BinaryHelper.ReadLV(fs);
                    if (rec.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(rec))
                        {
                            byte[] data = BinaryHelper.ReadLV(ms);
                            byte[] sha1 = BinaryHelper.ReadLV(ms);

                            byte[] calcSHA1 = Cryptography.SHA1.Hash(data);

                            string hexSHA1 = Hex.Encode(sha1);
                            string hexCalcSHA1 = Hex.Encode(calcSHA1);

                            if (hexSHA1 != hexCalcSHA1)
                                throw new TestFailedException(hexSHA1, hexCalcSHA1);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }

            return i;
        }

        internal static int HashStream(string file)
        {
            int i = 0;

            using (FileStream fs = StreamHelper.GetFileStreamOpen(file))
            {
                byte[] rec;
                do
                {
                    rec = BinaryHelper.ReadLV(fs);
                    if (rec.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(rec))
                        {
                            byte[] data = BinaryHelper.ReadLV(ms);
                            byte[] sha1 = BinaryHelper.ReadLV(ms);

                            byte[] calcSHA1;
                            using (MemoryStream ms2 = new MemoryStream(data))
                            {
                                calcSHA1 = Cryptography.SHA1.Hash(ms2);
                            }

                            string hexSHA1 = Hex.Encode(sha1);
                            string hexCalcSHA1 = Hex.Encode(calcSHA1);

                            if (hexSHA1 != hexCalcSHA1)
                                throw new TestFailedException(hexSHA1, hexCalcSHA1);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }

            return i;
        }
    }
}
