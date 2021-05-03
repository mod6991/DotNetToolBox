using DotNetToolBox.IO;
using System.IO;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class SHA3Tests
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
                            byte[] sha3 = BinaryHelper.ReadLV(ms);

                            byte[] calcSHA3 = Cryptography.SHA3.Hash(data);

                            string hexSHA3 = Hex.Encode(sha3);
                            string hexCalcSHA3 = Hex.Encode(calcSHA3);

                            if (hexSHA3 != hexCalcSHA3)
                                throw new TestFailedException(hexSHA3, hexCalcSHA3);
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
                            byte[] sha3 = BinaryHelper.ReadLV(ms);

                            byte[] calcSHA3;
                            using (MemoryStream ms2 = new MemoryStream(data))
                            {
                                calcSHA3 = Cryptography.SHA3.Hash(ms2);
                            }

                            string hexSHA3 = Hex.Encode(sha3);
                            string hexCalcSHA3 = Hex.Encode(calcSHA3);

                            if (hexSHA3 != hexCalcSHA3)
                                throw new TestFailedException(hexSHA3, hexCalcSHA3);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }

            return i;
        }
    }
}
