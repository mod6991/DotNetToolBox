#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012-2020 Josué Clément
//mod6991@gmail.com

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using DotNetToolBox.IO;
using DotNetToolBox.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNetToolBox.Cryptography
{
    public static class FileEnc
    {
        private const byte _version = 0x04;
        private const int _bufferSize = 4096;

        /// <summary>
        /// Encrypt with RSA key
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="keyName">Key name</param>
        public static void EncryptWithKey(Stream input, Stream output, RSACryptoServiceProvider rsa, string keyName, Action<int> notifyProgression = null)
        {
            byte[] key1 = RandomHelper.GenerateBytes(AES.KEY_SIZE);
            byte[] iv1 = RandomHelper.GenerateBytes(AES.IV_SIZE);
            byte[] key2 = RandomHelper.GenerateBytes(ChaCha20Rfc7539.KEY_SIZE);
            byte[] iv2 = RandomHelper.GenerateBytes(ChaCha20Rfc7539.NONCE_SIZE);

            byte[] keysTlvData = BinaryTlvWriter.BuildTlvList(new Dictionary<string, byte[]>()
            {
                { "K1", key1 },
                { "V1", iv1 },
                { "K2", key2 },
                { "V2", iv2 }
            }, 2);

            byte[] encKeysData = RSA.Encrypt(rsa, keysTlvData);

            BinaryHelper.WriteString(output, "ENCR!", Encoding.ASCII);
            BinaryTlvWriter writer = new BinaryTlvWriter(output, 2);
            writer.Write("VE", new byte[] { _version });
            writer.Write("KN", Encoding.ASCII.GetBytes(keyName));
            writer.Write("KS", encKeysData);

            bool padDone = false;
            int bytesRead;
            byte[] buffer = new byte[_bufferSize];
            byte[] rpad = new byte[0];
            byte[] xor = new byte[0];
            byte[] d1, d2;

            do
            {
                bytesRead = input.Read(buffer, 0, _bufferSize);

                if (bytesRead > 0)
                {
                    if (bytesRead == _bufferSize)
                    {
                        GeneratePadAndXor(bytesRead, buffer, ref rpad, ref xor);
                    }
                    else
                    {
                        byte[] smallBuffer = new byte[bytesRead];
                        Array.Copy(buffer, 0, smallBuffer, 0, bytesRead);
                        byte[] padData = Padding.Pad(smallBuffer, AES.BLOCK_SIZE, PaddingStyle.Pkcs7);
                        padDone = true;

                        GeneratePadAndXor(padData.Length, padData, ref rpad, ref xor);
                    }

                    d1 = ChaCha20Rfc7539.Encrypt(rpad, key2, iv2);
                    d2 = AES.Encrypt(xor, key1, iv1);

                    writer.Write("D1", d1);
                    writer.Write("D2", d2);
                }
            } while (bytesRead == _bufferSize);

            if (!padDone)
            {
                buffer = new byte[0];
                byte[] padData = Padding.Pad(buffer, AES.BLOCK_SIZE, PaddingStyle.Pkcs7);

                GeneratePadAndXor(AES.BLOCK_SIZE, padData, ref rpad, ref xor);

                d1 = ChaCha20Rfc7539.Encrypt(rpad, key2, iv2);
                d2 = AES.Encrypt(xor, key1, iv1);

                writer.Write("D1", d1);
                writer.Write("D2", d2);
            }
        }

        internal static void GeneratePadAndXor(int size, byte[] data, ref byte[] rpad, ref byte[] xor)
        {
            rpad = RandomHelper.GenerateBytes(size);
            xor = new byte[size];

            for (int i = 0; i < size; i++)
                xor[i] = (byte)(data[i] ^ rpad[i]);
        }

        /// <summary>
        /// Encrypt with password
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="password">Password</param>
        public static void EncryptWithPassword(Stream input, Stream output, string password, Action<int> notifyProgression = null)
        {
            //byte[] salt = RandomHelper.GenerateBytes(16);
            //byte[] key = PBKDF2.GenerateKeyFromPassword(AES.KEY_SIZE, password, salt);
            //byte[] iv = RandomHelper.GenerateBytes(AES.IV_SIZE);

            //BinaryHelper.WriteString(output, "ENCP!", Encoding.ASCII);
            //BinaryHelper.WriteByte(output, _version);
            //BinaryHelper.WriteByte(output, (byte)iv.Length);
            //BinaryHelper.WriteByte(output, (byte)salt.Length);
            //BinaryHelper.WriteBytes(output, iv);
            //BinaryHelper.WriteBytes(output, salt);

            //AES.Encrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, 4096, notifyProgression);
        }

        /// <summary>
        /// Decrypt with RSA key
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        public static void DecryptWithKey(Stream input, Stream output, RSACryptoServiceProvider rsa, Action<int> notifyProgression = null)
        {
            //input.Seek(5, SeekOrigin.Current); // Header
            //input.Seek(1, SeekOrigin.Current); // Version

            //byte keyNameLength = BinaryHelper.ReadByte(input);
            //Int16 encKeyLength = BinaryHelper.ReadInt16(input);
            //byte ivLength = BinaryHelper.ReadByte(input);

            //input.Seek(keyNameLength, SeekOrigin.Current); // Key name

            //byte[] encKey = BinaryHelper.ReadBytes(input, encKeyLength);
            //byte[] iv = BinaryHelper.ReadBytes(input, ivLength);

            //byte[] key = RSA.Decrypt(rsa, encKey);

            //if (notifyProgression != null)
            //    notifyProgression(5 + 1 + 1 + 2 + 1 + keyNameLength + encKeyLength + ivLength);

            //AES.Decrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, 4096, notifyProgression);
        }

        /// <summary>
        /// Decrypt with password
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="password">Password</param>
        public static void DecryptWithPassword(Stream input, Stream output, string password, Action<int> notifyProgression = null)
        {
            //input.Seek(5, SeekOrigin.Current); // Header
            //input.Seek(1, SeekOrigin.Current); // Version

            //byte ivLength = BinaryHelper.ReadByte(input);
            //byte saltLength = BinaryHelper.ReadByte(input);
            //byte[] iv = BinaryHelper.ReadBytes(input, ivLength);
            //byte[] salt = BinaryHelper.ReadBytes(input, saltLength);

            //byte[] key = PBKDF2.GenerateKeyFromPassword(AES.KEY_SIZE, password, salt);

            //if (notifyProgression != null)
            //    notifyProgression(5 + 1 + 1 + 1 + ivLength + saltLength);

            //AES.Decrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, 4096, notifyProgression);
        }
    }
}
