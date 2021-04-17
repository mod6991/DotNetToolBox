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
        private const byte _version = 0x03;

        /// <summary>
        /// Encrypt with RSA key
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="keyName">Key name</param>
        public static void EncryptWithKey(Stream input, Stream output, RSACryptoServiceProvider rsa, string keyName, Action<int> notifyProgression = null)
        {
            //byte[] key = RandomHelper.GenerateBytes(AES.KEY_SIZE);
            //byte[] iv = RandomHelper.GenerateBytes(AES.IV_SIZE);

            //byte[] encKey = RSA.Encrypt(rsa, key);

            //byte[] keyNameData = Encoding.ASCII.GetBytes(keyName);

            //BinaryHelper.WriteString(output, "ENCR!", Encoding.ASCII);
            //BinaryHelper.WriteByte(output, _version);
            //BinaryHelper.WriteByte(output, (byte)keyNameData.Length);
            //BinaryHelper.WriteInt16(output, (Int16)encKey.Length);
            //BinaryHelper.WriteByte(output, (byte)iv.Length);
            //BinaryHelper.WriteBytes(output, keyNameData);
            //BinaryHelper.WriteBytes(output, encKey);
            //BinaryHelper.WriteBytes(output, iv);

            //AES.Encrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, 4096, notifyProgression);
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
