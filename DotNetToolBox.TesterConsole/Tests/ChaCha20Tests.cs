﻿using DotNetToolBox.IO;
using System.IO;

namespace DotNetToolBox.TesterConsole.Tests
{
    internal static class ChaCha20Tests
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

                            byte[] calcEnc = Cryptography.ChaCha20.Encrypt(data, key, iv);

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

        internal static int EncryptStream(string file)
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

                            byte[] calcEnc;
                            using (MemoryStream msIn = new MemoryStream(data))
                            {
                                using (MemoryStream msOut = new MemoryStream())
                                {
                                    Cryptography.ChaCha20.Encrypt(msIn, msOut, key, iv);
                                    calcEnc = msOut.ToArray();
                                }
                            }

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

                            byte[] calcDec = Cryptography.ChaCha20.Decrypt(enc, key, iv);

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

        internal static int DecryptStream(string file)
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

                            byte[] calcDec;
                            using (MemoryStream msIn = new MemoryStream(enc))
                            {
                                using (MemoryStream msOut = new MemoryStream())
                                {
                                    Cryptography.ChaCha20.Decrypt(msIn, msOut, key, iv);
                                    calcDec = msOut.ToArray();
                                }
                            }

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
