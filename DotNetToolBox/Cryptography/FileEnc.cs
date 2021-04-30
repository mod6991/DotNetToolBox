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
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNetToolBox.Cryptography
{
    public static class FileEnc
    {
        private const byte _version = 0x04;
        private const int _bufferSize = 4096;
        private const PaddingStyle _paddingStyle = PaddingStyle.Pkcs7;

        /// <summary>
        /// Encrypt with RSA key
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="keyName">Key name</param>
        public static void EncryptWithKey(Stream input, Stream output, RSACryptoServiceProvider rsa, string keyName)
        {
            byte[] key1 = RandomHelper.GenerateBytes(ChaCha20Rfc7539.KEY_SIZE);
            byte[] iv1 = RandomHelper.GenerateBytes(ChaCha20Rfc7539.NONCE_SIZE);
            byte[] key2 = RandomHelper.GenerateBytes(AES.KEY_SIZE);
            byte[] iv2 = RandomHelper.GenerateBytes(AES.IV_SIZE);

            byte[] keysData;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryHelper.WriteLV(ms, key1);
                BinaryHelper.WriteLV(ms, iv1);
                BinaryHelper.WriteLV(ms, key2);
                BinaryHelper.WriteLV(ms, iv2);
                keysData = ms.ToArray();
            }

            byte[] encKeysData = RSA.Encrypt(rsa, keysData);

            BinaryHelper.WriteString(output, "ENCR!", Encoding.ASCII);
            BinaryHelper.WriteByte(output, _version);
            BinaryHelper.WriteLV(output, Encoding.ASCII.GetBytes(keyName));
            BinaryHelper.WriteLV(output, encKeysData);

            bool padDone = false;
            int bytesRead;
            byte[] buffer = new byte[_bufferSize];

            do
            {
                bytesRead = input.Read(buffer, 0, _bufferSize);

                if (bytesRead > 0)
                {
                    if (bytesRead == _bufferSize)
                    {
                        XorEncryptAndWrite(output, bytesRead, buffer, key1, iv1, key2, iv2);
                    }
                    else
                    {
                        byte[] smallBuffer = new byte[bytesRead];
                        Array.Copy(buffer, 0, smallBuffer, 0, bytesRead);
                        byte[] padData = Padding.Pad(smallBuffer, AES.BLOCK_SIZE, _paddingStyle);
                        padDone = true;

                        XorEncryptAndWrite(output, padData.Length, padData, key1, iv1, key2, iv2);
                    }
                }
            } while (bytesRead == _bufferSize);

            if (!padDone)
            {
                buffer = new byte[0];
                byte[] padData = Padding.Pad(buffer, AES.BLOCK_SIZE, _paddingStyle);

                XorEncryptAndWrite(output, AES.BLOCK_SIZE, padData, key1, iv1, key2, iv2);
            }

            BinaryHelper.WriteLV(output, new byte[0]);
        }

        internal static void XorEncryptAndWrite(Stream output, int size, byte[] data, byte[] key1, byte[] iv1, byte[] key2, byte[] iv2)
        {
            byte[] rpad = RandomHelper.GenerateBytes(size);
            byte[] xor = new byte[size];

            for (int i = 0; i < size; i++)
                xor[i] = (byte)(data[i] ^ rpad[i]);

            byte[] d1 = ChaCha20Rfc7539.Encrypt(rpad, key1, iv1);
            byte[] d2 = AES.Encrypt(xor, key2, iv2);

            BinaryHelper.WriteLV(output, d1);
            BinaryHelper.WriteLV(output, d2);
        }

        /// <summary>
        /// Encrypt with password
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="password">Password</param>
        public static void EncryptWithPassword(Stream input, Stream output, string password)
        {
            byte[] salt = RandomHelper.GenerateBytes(16);
            byte[] key = PBKDF2.GenerateKeyFromPassword(AES.KEY_SIZE, password, salt);
            byte[] iv = RandomHelper.GenerateBytes(AES.IV_SIZE);

            BinaryHelper.WriteString(output, "ENCP!", Encoding.ASCII);
            BinaryHelper.WriteByte(output, _version);
            BinaryHelper.WriteByte(output, (byte)iv.Length);
            BinaryHelper.WriteByte(output, (byte)salt.Length);
            BinaryHelper.WriteBytes(output, iv);
            BinaryHelper.WriteBytes(output, salt);

            AES.Encrypt(input, output, key, iv, _paddingStyle);
        }

        /// <summary>
        /// Decrypt with RSA key
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        public static void DecryptWithKey(Stream input, Stream output, RSACryptoServiceProvider rsa)
        {
            input.Seek(5, SeekOrigin.Current); // Header
            input.Seek(1, SeekOrigin.Current); // Version

            BinaryHelper.ReadLV(input);
            byte[] encKeysData = BinaryHelper.ReadLV(input);
            byte[] keysData = RSA.Decrypt(rsa, encKeysData);

            byte[] key1, iv1, key2, iv2;
            using (MemoryStream ms = new MemoryStream(keysData))
            {
                key1 = BinaryHelper.ReadLV(ms);
                iv1 = BinaryHelper.ReadLV(ms);
                key2 = BinaryHelper.ReadLV(ms);
                iv2 = BinaryHelper.ReadLV(ms);
            }

            byte[] d1, d2;
            byte[] backup = null;

            do
            {
                d1 = BinaryHelper.ReadLV(input);
                if (d1.Length > 0)
                {
                    if (backup != null)
                        output.Write(backup, 0, backup.Length);

                    byte[] rpad = ChaCha20Rfc7539.Decrypt(d1, key1, iv1);
                    d2 = BinaryHelper.ReadLV(input);
                    byte[] xor = AES.Decrypt(d2, key2, iv2);

                    byte[] data = new byte[rpad.Length];
                    for (int i = 0; i < rpad.Length; i++)
                        data[i] = (byte)(rpad[i] ^ xor[i]);

                    backup = new byte[data.Length];
                    Array.Copy(data, 0, backup, 0, data.Length);
                }
                else
                {
                    byte[] unpadData = Padding.Unpad(backup, AES.BLOCK_SIZE, _paddingStyle);
                    output.Write(unpadData, 0, unpadData.Length);
                }

            } while (d1.Length > 0);
        }

        /// <summary>
        /// Decrypt with password
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="password">Password</param>
        public static void DecryptWithPassword(Stream input, Stream output, string password)
        {
            input.Seek(5, SeekOrigin.Current); // Header
            input.Seek(1, SeekOrigin.Current); // Version

            byte ivLength = BinaryHelper.ReadByte(input);
            byte saltLength = BinaryHelper.ReadByte(input);
            byte[] iv = BinaryHelper.ReadBytes(input, ivLength);
            byte[] salt = BinaryHelper.ReadBytes(input, saltLength);

            byte[] key = PBKDF2.GenerateKeyFromPassword(AES.KEY_SIZE, password, salt);

            AES.Decrypt(input, output, key, iv, _paddingStyle);
        }
    }
}