using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using DotNetToolBox.Utils;
using System.IO;
using System.Security.Cryptography;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class FileEncTests
    {
        internal static int EncryptDecrypt(string file)
        {
            int i = 0;

            RSACryptoServiceProvider rsa = Cryptography.RSA.GenerateKeyPair();

            for(; i < 300; i++)
            {
                byte[] data = RandomHelper.GenerateBytes(i * 16);

                byte[] enc;
                using(MemoryStream msIn = new MemoryStream(data))
                {
                    using (MemoryStream msOut = new MemoryStream())
                    {
                        FileEnc.EncryptWithKey(msIn, msOut, rsa, "testkey01");
                        enc = msOut.ToArray();
                    }
                }

                byte[] dec;
                using(MemoryStream msIn = new MemoryStream(enc))
                {
                    using (MemoryStream msOut = new MemoryStream())
                    {
                        FileEnc.DecryptWithKey(msIn, msOut, rsa);
                        dec = msOut.ToArray();
                    }
                }

                string hexData = Hex.Encode(data);
                string hexDec = Hex.Encode(dec);

                if (hexData != hexDec)
                    throw new TestFailedException(hexData, hexDec);
            }

            return i;
        }
    }
}
