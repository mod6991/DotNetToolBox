using DotNetToolBox.IO;
using System.IO;
using System.Text;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class Base64Tests
    {
        internal static int Encode(string file)
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
                            string b64 = Encoding.ASCII.GetString(BinaryHelper.ReadLV(ms));

                            string calcB64 = Base64.Encode(data);

                            if (b64 != calcB64)
                                throw new TestFailedException(b64, calcB64);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }
            return i;
        }

        internal static int Decode(string file)
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
                            string b64 = Encoding.ASCII.GetString(BinaryHelper.ReadLV(ms));

                            byte[] calcData = Base64.Decode(b64);

                            string hexData = Hex.Encode(data);
                            string hexCalcData = Hex.Encode(calcData);
                            if (hexData != hexCalcData)
                                throw new TestFailedException(hexData, hexCalcData);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }

            return i;
        }
    }
}
