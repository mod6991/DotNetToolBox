#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012  Josué Clément
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

using System.IO;
using System.Security.Cryptography;

namespace DotNetToolBox.Cryptography
{
    public static class DESEncryptor
    {
        public const int KEY_SIZE = 8;
        public const int IV_SIZE = 8;

        /// <summary>
        /// Encrypt data with the DES algorithm
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Encrypt(Stream input, Stream output, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Mode = cipherMode;
                des.Padding = paddingMode;
                ICryptoTransform cryptor = des.CreateEncryptor(key, iv);
                using (CryptoStream cs = new CryptoStream(output, cryptor, CryptoStreamMode.Write))
                {
                    IO.StreamHelper.WriteStream(input, cs, bufferSize);
                }
            }
        }

        /// <summary>
        /// Encrypt data with the DES algorithm
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Encrypt(Stream input, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Encrypt(input, ms, key, iv, cipherMode, paddingMode, bufferSize);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Encrypt data with the DES algorithm
        /// </summary>
        /// <param name="data">Input data</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Encrypt(ms, key, iv, cipherMode, paddingMode, bufferSize);
            }
        }

        /// <summary>
        /// Encrypt data with the DES algorithm
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Encrypt(string inputFile, string outputFile, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    Encrypt(input, output, key, iv, cipherMode, paddingMode, bufferSize);
                }
            }
        }

        /// <summary>
        /// Decrypt data with the DES algorithm
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decrypt(Stream input, Stream output, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Mode = cipherMode;
                des.Padding = paddingMode;
                ICryptoTransform cryptor = des.CreateDecryptor(key, iv);
                using (CryptoStream cs = new CryptoStream(output, cryptor, CryptoStreamMode.Write))
                {
                    IO.StreamHelper.WriteStream(input, cs, bufferSize);
                }
            }
        }

        /// <summary>
        /// Decrypt data with the DES algorithm
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Decrypt(Stream input, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Decrypt(input, ms, key, iv, cipherMode, paddingMode, bufferSize);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decrypt data with the DES algorithm
        /// </summary>
        /// <param name="data">Input data</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Decrypt(ms, key, iv, cipherMode, paddingMode, bufferSize);
            }
        }

        /// <summary>
        /// Decrypt data with the DES algorithm
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decrypt(string inputFile, string outputFile, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    Decrypt(input, output, key, iv, cipherMode, paddingMode, bufferSize);
                }
            }
        }

        /// <summary>
        /// Generate a random Key and IV
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        public static void GenerateKeyIV(out byte[] key, out byte[] iv)
        {
            key = new byte[KEY_SIZE];
            iv = new byte[IV_SIZE];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }
        }

        /// <summary>
        /// Generate a random IV
        /// </summary>
        /// <param name="iv">IV</param>
        public static void GenerateIV(out byte[] iv)
        {
            iv = new byte[IV_SIZE];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }
        }

        /// <summary>
        /// Generate a Key from Password and Salt
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="salt">Salt</param>
        /// <param name="key">Key</param>
        /// <param name="iterations">Iterations</param>
        public static void GenerateKeyFromPassword(string password, byte[] salt, out byte[] key, int iterations = 10000)
        {
            key = new byte[KEY_SIZE];

            using (Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                key = pdb.GetBytes(KEY_SIZE);
            }
        }
    }
}
