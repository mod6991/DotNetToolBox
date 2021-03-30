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

using System;
using System.IO;
using System.Security.Cryptography;

namespace DotNetToolBox.Cryptography
{
    public static class DES
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
        /// <param name="notifyProgression">Notify progression method</param>
        public static void Encrypt(Stream input, Stream output, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Mode = cipherMode;
                des.Padding = paddingMode;
                ICryptoTransform cryptor = des.CreateEncryptor(key, iv);
                using (CryptoStream cs = new CryptoStream(output, cryptor, CryptoStreamMode.Write))
                {
                    IO.StreamHelper.WriteStream(input, cs, bufferSize, notifyProgression);
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
        /// <param name="notifyProgression">Notify progression method</param>
        public static void Decrypt(Stream input, Stream output, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Mode = cipherMode;
                des.Padding = paddingMode;
                ICryptoTransform cryptor = des.CreateDecryptor(key, iv);
                using (CryptoStream cs = new CryptoStream(output, cryptor, CryptoStreamMode.Write))
                {
                    IO.StreamHelper.WriteStream(input, cs, bufferSize, notifyProgression);
                }
            }
        }
    }
}
