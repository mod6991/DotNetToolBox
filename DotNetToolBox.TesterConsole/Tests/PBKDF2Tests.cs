using DotNetToolBox.IO;
using System.IO;
using System.Text;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class PBKDF2Tests
    {
        internal static int GenerateKey(string file)
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
                            string password = Encoding.ASCII.GetString(BinaryHelper.ReadLV(ms));
                            byte[] salt = BinaryHelper.ReadLV(ms);
                            byte[] key = BinaryHelper.ReadLV(ms);

                            byte[] calcKey = Cryptography.PBKDF2.GenerateKeyFromPassword(32, password, salt, 50000);

                            string hexKey = Hex.Encode(key);
                            string hexCalcKey = Hex.Encode(calcKey);

                            if (hexKey != hexCalcKey)
                                throw new TestFailedException(hexKey, hexCalcKey);
                        }
                        i++;
                    }
                } while (rec.Length > 0 && i < 10);
            }

            return i;
        }
    }
}
