using DotNetToolBox.IO;
using System.IO;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class PaddingPkcs7Tests
    {
        internal static int Pad(string file)
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
                            byte[] padded = BinaryHelper.ReadLV(ms);

                            byte[] calcPadded = Cryptography.Padding.Pad(data, 16, Cryptography.PaddingStyle.Pkcs7);

                            string hexPadded = Hex.Encode(padded);
                            string hexCalcPadded = Hex.Encode(calcPadded);

                            if (hexPadded != hexCalcPadded)
                                throw new TestFailedException(hexPadded, hexCalcPadded);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }

            return i;
        }

        internal static int Unpad(string file)
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
                            byte[] padded = BinaryHelper.ReadLV(ms);

                            byte[] calcUnpadded = Cryptography.Padding.Unpad(padded, 16, Cryptography.PaddingStyle.Pkcs7);

                            string hexData = Hex.Encode(data);
                            string hexCalcUnpadded = Hex.Encode(calcUnpadded);

                            if (hexData != hexCalcUnpadded)
                                throw new TestFailedException(hexData, hexCalcUnpadded);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }

            return i;
        }
    }
}
