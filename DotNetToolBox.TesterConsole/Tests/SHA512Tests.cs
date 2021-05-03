using DotNetToolBox.IO;
using System.IO;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class SHA512Tests
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
                            byte[] sha512 = BinaryHelper.ReadLV(ms);

                            byte[] calcSHA512 = Cryptography.SHA512.Hash(data);

                            string hexSHA512 = Hex.Encode(sha512);
                            string hexCalcSHA512 = Hex.Encode(calcSHA512);

                            if (hexSHA512 != hexCalcSHA512)
                                throw new TestFailedException(hexSHA512, hexCalcSHA512);
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
                            byte[] sha512 = BinaryHelper.ReadLV(ms);

                            byte[] calcSHA512;
                            using (MemoryStream ms2 = new MemoryStream(data))
                            {
                                calcSHA512 = Cryptography.SHA512.Hash(ms2);
                            }

                            string hexSHA512 = Hex.Encode(sha512);
                            string hexCalcSHA512 = Hex.Encode(calcSHA512);

                            if (hexSHA512 != hexCalcSHA512)
                                throw new TestFailedException(hexSHA512, hexCalcSHA512);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }

            return i;
        }
    }
}
