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

using System;
using System.IO;

namespace DotNetToolBox.Cryptography
{
    public static class VernamEncryptor
    {
        private static byte[] EncryptDecryptInternal(byte[] input, byte[] key, int readBytes)
        {
            byte[] result = new byte[readBytes];

            for (int i = 0; i < readBytes; i++)
                result[i] = (byte)(input[i] ^ key[i]);

            return result;
        }

        /// <summary>
        /// Encrypt/Decrypt data with the Vernam algorithm
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="key">Key Stream</param>
        /// <param name="output">OutputStream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void EncryptDecrypt(Stream input, Stream key, Stream output, int bufferSize = 4096)
        {
            byte[] inputBuffer = new byte[bufferSize];
            byte[] keyBuffer = new byte[bufferSize];
            byte[] resultBuffer = new byte[bufferSize];

            int inputBytesRead;
            int keyBytesRead;

            do
            {
                inputBytesRead = input.Read(inputBuffer, 0, bufferSize);
                keyBytesRead = key.Read(keyBuffer, 0, bufferSize);

                if (inputBytesRead != keyBytesRead)
                    throw new Exception("Input and key do not have the same size !");

                if (inputBytesRead > 0)
                    resultBuffer = EncryptDecryptInternal(inputBuffer, keyBuffer, inputBytesRead);
                    output.Write(resultBuffer, 0, inputBytesRead);
            } while (inputBytesRead == bufferSize);
        }

        /// <summary>
        /// Encrypt/Decrypt data with the Vernam algorithm
        /// </summary>
        /// <param name="input">Input data</param>
        /// <param name="key">Key data</param>
        public static byte[] EncryptDecrypt(byte[] input, byte[] key)
        {
            if (input == null)
                throw new ArgumentException("input");
            if (key == null)
                throw new ArgumentException("key");

            if (input.Length != key.Length)
                throw new Exception("Input and key do not have the same size !");

            byte[] result = new byte[input.Length];

            for (int i = 0, l = input.Length; i < l; i++)
                result[i] = (byte)(input[i] ^ key[i]);

            return result;
        }
    }
}
