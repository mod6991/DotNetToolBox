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

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;

namespace DotNetToolBox.Cryptography
{
    public static class AES
    {
        public const int KEY_SIZE = 32;
        public const int IV_SIZE = 16;
        public const int BLOCK_SIZE = 16;

        /// <summary>
        /// Encrypt data with AES-CBC
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <returns>Encrypted data</returns>
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            byte[] enc = new byte[data.Length];

            IBufferedCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), iv, 0, iv.Length);
            cipher.Init(true, parameters);
            cipher.ProcessBytes(data, enc, 0);

            return enc;
        }

        /// <summary>
        /// Encrypt stream with AES-CBC
        /// </summary>
        /// <param name="input">Input stream to encrypt</param>
        /// <param name="output">Output stream</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="paddingStyle">Padding</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Encrypt(Stream input, Stream output, byte[] key, byte[] iv, PaddingStyle paddingStyle = PaddingStyle.Pkcs7, int bufferSize = 4096)
        {
            IBufferedCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), iv, 0, iv.Length);
            cipher.Init(true, parameters);

            bool padDone = false;
            int bytesRead;
            byte[] buffer = new byte[bufferSize];
            byte[] enc = new byte[bufferSize];

            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);

                if (bytesRead > 0 && bytesRead == bufferSize)
                {
                    cipher.ProcessBytes(buffer, enc, 0);
                    output.Write(enc, 0, bytesRead);
                }
                else if (bytesRead > 0)
                {
                    byte[] smallBuffer = new byte[bytesRead];
                    Array.Copy(buffer, 0, smallBuffer, 0, bytesRead);
                    byte[] padData = Padding.Pad(smallBuffer, BLOCK_SIZE, paddingStyle);
                    cipher.ProcessBytes(padData, enc, 0);
                    output.Write(enc, 0, padData.Length);
                    padDone = true;
                }
            } while (bytesRead == bufferSize);

            if (!padDone)
            {
                buffer = new byte[0];
                byte[] padData = Padding.Pad(buffer, BLOCK_SIZE, paddingStyle);
                cipher.ProcessBytes(padData, enc, 0);
                output.Write(enc, 0, padData.Length);
            }
        }

        /// <summary>
        /// Decrypt data with AES-CBC
        /// </summary>
        /// <param name="data">Data to decrypt</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <returns>Decrypted data</returns>
        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            byte[] dec = new byte[data.Length];

            IBufferedCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), iv, 0, iv.Length);
            cipher.Init(false, parameters);
            cipher.ProcessBytes(data, dec, 0);

            return dec;
        }

        /// <summary>
        /// Decrypt stream with AES-CBC
        /// </summary>
        /// <param name="input">Input stream to decrypt</param>
        /// <param name="output">Output stream</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="paddingStyle">Padding</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decrypt(Stream input, Stream output, byte[] key, byte[] iv, PaddingStyle paddingStyle = PaddingStyle.Pkcs7, int bufferSize = 4096)
        {
            IBufferedCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), iv, 0, iv.Length);
            cipher.Init(false, parameters);

            byte[] backup = null;
            int bytesRead;
            byte[] buffer = new byte[bufferSize];
            byte[] dec = new byte[bufferSize];

            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);

                if (bytesRead > 0)
                {
                    if (backup != null)
                    {
                        output.Write(backup, 0, backup.Length);
                        backup = null;
                    }

                    if (bytesRead == bufferSize)
                    {
                        cipher.ProcessBytes(buffer, dec, 0);
                        backup = new byte[bytesRead];
                        Array.Copy(dec, 0, backup, 0, bytesRead);
                    }
                    else
                    {
                        dec = new byte[bytesRead];
                        byte[] smallBuffer = new byte[bytesRead];
                        Array.Copy(buffer, 0, smallBuffer, 0, bytesRead);
                        cipher.ProcessBytes(smallBuffer, dec, 0);
                        byte[] unpadData = Padding.Unpad(dec, BLOCK_SIZE, paddingStyle);
                        output.Write(unpadData, 0, unpadData.Length);
                    }
                }
                else
                {
                    if (backup != null)
                    {
                        byte[] unpadData = Padding.Unpad(backup, BLOCK_SIZE, paddingStyle);
                        output.Write(unpadData, 0, unpadData.Length);
                    }
                }
            } while (bytesRead == bufferSize);
        }
    }
}