﻿using DotNetToolBox.IO;
using System.IO;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class MD5Tests
    {
        internal static void Hash(string file)
        {
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
                            byte[] md5 = BinaryHelper.ReadLV(ms);

                            byte[] calcMD5 = Cryptography.MD5.Hash(data);

                            string hexMD5 = Hex.Encode(md5);
                            string hexCalcMD5 = Hex.Encode(calcMD5);

                            if (hexMD5 != hexCalcMD5)
                                throw new TestFailedException(hexMD5, hexCalcMD5);
                        }
                    }
                } while (rec.Length > 0);
            }
        }

        internal static void HashStream(string file)
        {
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
                            byte[] md5 = BinaryHelper.ReadLV(ms);

                            byte[] calcMD5;
                            using (MemoryStream ms2 = new MemoryStream(data))
                            {
                                calcMD5 = Cryptography.MD5.Hash(ms2);
                            }

                            string hexMD5 = Hex.Encode(md5);
                            string hexCalcMD5 = Hex.Encode(calcMD5);

                            if (hexMD5 != hexCalcMD5)
                                throw new TestFailedException(hexMD5, hexCalcMD5);
                        }
                    }
                } while (rec.Length > 0);
            }
        }
    }
}
