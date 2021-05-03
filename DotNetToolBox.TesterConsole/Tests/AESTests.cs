using DotNetToolBox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class AESTests
    {
        internal static int Encrypt(string file)
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
                            byte[] key = BinaryHelper.ReadLV(ms);
                            byte[] iv = BinaryHelper.ReadLV(ms);
                            byte[] data = BinaryHelper.ReadLV(ms);
                            byte[] enc = BinaryHelper.ReadLV(ms);

                            byte[] calcEnc = Cryptography.AES.Encrypt(data, key, iv);

                            string hexEnc = Hex.Encode(enc);
                            string hexCalcEnc = Hex.Encode(calcEnc);

                            if (hexEnc != hexCalcEnc)
                                throw new TestFailedException(hexEnc, hexCalcEnc);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }
            return i;
        }

        internal static int Decrypt(string file)
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
                            byte[] key = BinaryHelper.ReadLV(ms);
                            byte[] iv = BinaryHelper.ReadLV(ms);
                            byte[] data = BinaryHelper.ReadLV(ms);
                            byte[] enc = BinaryHelper.ReadLV(ms);

                            byte[] calcDec = Cryptography.AES.Decrypt(enc, key, iv);

                            string hexDec = Hex.Encode(data);
                            string hexCalcDec = Hex.Encode(calcDec);

                            if (hexDec != hexCalcDec)
                                throw new TestFailedException(hexDec, hexCalcDec);
                        }
                        i++;
                    }
                } while (rec.Length > 0);
            }

            return i;
        }
    }
}
