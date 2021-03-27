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
        private static byte[] _headerKey = Encoding.ASCII.GetBytes("ENC!R!");
        private static int _headerKeyLen = _headerKey.Length;
        private static byte[] _headerPass = Encoding.ASCII.GetBytes("ENC!P!");
        private static int _headerPassLen = _headerPass.Length;
        private static byte[] _version = new byte[] { 0x03 };

        /// <summary>
        /// Encrypt with RSA key
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="keyName">Key name</param>
        public static void EncryptWithKey(Stream input, Stream output, RSACryptoServiceProvider rsa, string keyName)
        {
            byte[] key, iv;
            AES.GenerateKeyIV(out key, out iv);

            byte[] encKey = RSA.Encrypt(rsa, key);

            byte[] keyNameData = Encoding.ASCII.GetBytes(keyName);

            output.Write(_headerKey, 0, _headerKeyLen);
            output.Write(_version, 0, 1);
            output.Write(new byte[] { (byte)keyNameData.Length }, 0, 1);
            output.Write(BitConverter.GetBytes(encKey.Length), 0, 4);
            output.Write(new byte[] { (byte)iv.Length }, 0, 1);
            output.Write(keyNameData, 0, keyNameData.Length);
            output.Write(encKey, 0, encKey.Length);
            output.Write(iv, 0, iv.Length);

            AES.Encrypt(input, output, key, iv);
        }

        /// <summary>
        /// Encrypt file with RSA key
        /// </summary>
        /// <param name="inputFile">Input file path</param>
        /// <param name="outputFile">Output file path</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="keyName">Key name</param>
        public static void EncryptWithKey(string inputFile, string outputFile, RSACryptoServiceProvider rsa, string keyName)
        {
            using (FileStream fsIn = StreamHelper.GetFileStreamOpen(inputFile))
            {
                using (FileStream fsOut = StreamHelper.GetFileStreamCreate(outputFile))
                {
                    EncryptWithKey(fsIn, fsOut, rsa, keyName);
                }
            }
        }

        /// <summary>
        /// Encrypt with password
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="password">Password</param>
        public static void EncryptWithPassword(Stream input, Stream output, string password)
        {
            byte[] key, iv, salt;
            salt = RandomHelper.GenerateBytes(16);
            AES.GenerateKeyFromPassword(password, salt, out key);
            AES.GenerateIV(out iv);

            output.Write(_headerPass, 0, _headerPassLen);
            output.Write(_version, 0, 1);
            output.Write(new byte[] { (byte)iv.Length }, 0, 1);
            output.Write(new byte[] { (byte)salt.Length }, 0, 1);
            output.Write(iv, 0, iv.Length);
            output.Write(salt, 0, salt.Length);

            AES.Encrypt(input, output, key, iv);
        }

        /// <summary>
        /// Encrypt file with password
        /// </summary>
        /// <param name="inputFile">Input file path</param>
        /// <param name="outputFile">Output file path</param>
        /// <param name="password">Password</param>
        public static void EncryptWithPassword(string inputFile, string outputFile, string password)
        {
            using (FileStream fsIn = StreamHelper.GetFileStreamOpen(inputFile))
            {
                using (FileStream fsOut = StreamHelper.GetFileStreamCreate(outputFile))
                {
                    EncryptWithPassword(fsIn, fsOut, password);
                }
            }
        }

        /// <summary>
        /// Decrypt with RSA key
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        public static void DecryptWithKey(Stream input, Stream output, RSACryptoServiceProvider rsa)
        {
            byte[] buffer;

            input.Seek(_headerKeyLen, SeekOrigin.Current);
            input.Seek(1, SeekOrigin.Current);

            buffer = new byte[1];
            input.Read(buffer, 0, 1);
            byte keyNameLength = buffer[0];

            buffer = new byte[4];
            input.Read(buffer, 0, 4);
            int encKeyLength = BitConverter.ToInt32(buffer, 0);

            buffer = new byte[1];
            input.Read(buffer, 0, 1);
            byte ivLength = buffer[0];

            input.Seek(keyNameLength, SeekOrigin.Current);

            byte[] encKey = new byte[encKeyLength];
            input.Read(encKey, 0, encKeyLength);

            byte[] iv = new byte[ivLength];
            input.Read(iv, 0, ivLength);

            byte[] key = RSA.Decrypt(rsa, encKey);

            AES.Decrypt(input, output, key, iv);
        }

        /// <summary>
        /// Decrypt file with RSA key
        /// </summary>
        /// <param name="inputFile">Input file path</param>
        /// <param name="outputFile">Output file path</param>
        /// <param name="rsa">RSA key</param>
        public static void DecryptWithKey(string inputFile, string outputFile, RSACryptoServiceProvider rsa)
        {
            using (FileStream fsIn = StreamHelper.GetFileStreamOpen(inputFile))
            {
                using (FileStream fsOut = StreamHelper.GetFileStreamCreate(outputFile))
                {
                    DecryptWithKey(fsIn, fsOut, rsa);
                }
            }
        }

        /// <summary>
        /// Decrypt with password
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="password">Password</param>
        public static void DecryptWithPassword(Stream input, Stream output, string password)
        {
            byte[] buffer, key;

            input.Seek(_headerPassLen, SeekOrigin.Current);
            input.Seek(1, SeekOrigin.Current);

            buffer = new byte[1];
            input.Read(buffer, 0, 1);
            byte ivLength = buffer[0];

            buffer = new byte[1];
            input.Read(buffer, 0, 1);
            byte saltLength = buffer[0];

            byte[] iv = new byte[ivLength];
            input.Read(iv, 0, ivLength);

            byte[] salt = new byte[saltLength];
            input.Read(salt, 0, saltLength);

            AES.GenerateKeyFromPassword(password, salt, out key);

            AES.Decrypt(input, output, key, iv);
        }

        /// <summary>
        /// Decrypt file with password
        /// </summary>
        /// <param name="inputFile">Input file path</param>
        /// <param name="outputFile">Output file path</param>
        /// <param name="password">Password</param>
        public static void DecryptWithPassword(string inputFile, string outputFile, string password)
        {
            using (FileStream fsIn = StreamHelper.GetFileStreamOpen(inputFile))
            {
                using (FileStream fsOut = StreamHelper.GetFileStreamCreate(outputFile))
                {
                    DecryptWithPassword(fsIn, fsOut, password);
                }
            }
        }
    }
}
