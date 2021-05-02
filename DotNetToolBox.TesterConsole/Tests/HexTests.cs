using DotNetToolBox.IO;
using System.IO;
using System.Text;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class HexTests
    {
        internal static void Encode(string file)
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
                            string hex = Encoding.ASCII.GetString(BinaryHelper.ReadLV(ms));

                            string calcHex = Hex.Encode(data);

                            if (hex != calcHex)
                                throw new TestFailedException(hex, calcHex);
                        }
                    }
                } while (rec.Length > 0);
            }
        }

        internal static void Decode(string file)
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
                            string hex = Encoding.ASCII.GetString(BinaryHelper.ReadLV(ms));

                            byte[] calcData = Hex.Decode(hex);

                            string b64Data = Base64.Encode(data);
                            string b64CalcData = Base64.Encode(calcData);
                            if (b64Data != b64CalcData)
                                throw new TestFailedException(b64Data, b64CalcData);
                        }
                    }
                } while (rec.Length > 0);
            }
        }
    }
}
