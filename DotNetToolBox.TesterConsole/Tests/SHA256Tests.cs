using DotNetToolBox.IO;
using System.IO;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class SHA256Tests
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
                            byte[] sha256 = BinaryHelper.ReadLV(ms);

                            byte[] calcSHA256 = Cryptography.SHA256.Hash(data);

                            string hexSHA256 = Hex.Encode(sha256);
                            string hexCalcSHA256 = Hex.Encode(calcSHA256);

                            if (hexSHA256 != hexCalcSHA256)
                                throw new TestFailedException(hexSHA256, hexCalcSHA256);
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
                            byte[] sha256 = BinaryHelper.ReadLV(ms);

                            byte[] calcSHA256;
                            using (MemoryStream ms2 = new MemoryStream(data))
                            {
                                calcSHA256 = Cryptography.SHA256.Hash(ms2);
                            }

                            string hexSHA256 = Hex.Encode(sha256);
                            string hexCalcSHA256 = Hex.Encode(calcSHA256);

                            if (hexSHA256 != hexCalcSHA256)
                                throw new TestFailedException(hexSHA256, hexCalcSHA256);
                        }
                    }
                } while (rec.Length > 0);
            }
        }
    }
}
