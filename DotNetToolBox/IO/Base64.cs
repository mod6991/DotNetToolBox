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
using System.Text;

namespace DotNetToolBox.IO
{
    public static class Base64
    {
        /// <summary>
        /// Encode data with base64
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="output">Output Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Encode(Stream input, Stream output, int bufferSize = 4096)
        {
            ICryptoTransform cryptor = new ToBase64Transform();
            using (CryptoStream cs = new CryptoStream(output, cryptor, CryptoStreamMode.Write))
            {
                StreamHelper.WriteStream(input, cs, bufferSize);
            }
        }

        /// <summary>
        /// Encode data with base64
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static string Encode(Stream input, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Encode(input, ms, bufferSize);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Encode data with base64
        /// </summary>
        /// <param name="inputData">Data to encode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static string Encode(byte[] inputData, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream(inputData))
            {
                return Encode(ms, bufferSize);
            }
        }

        /// <summary>
        /// Encode data with base64
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Encode(string inputFile, string outputFile, int bufferSize = 4096)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    Encode(input, output, bufferSize);
                }
            }
        }

        /// <summary>
        /// Decode data with base64
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="output">Output Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decode(Stream input, Stream output, int bufferSize = 4096)
        {
            ICryptoTransform cryptor = new FromBase64Transform();
            using (CryptoStream cs = new CryptoStream(output, cryptor, CryptoStreamMode.Write))
            {
                StreamHelper.WriteStream(input, cs, bufferSize);
            }
        }

        /// <summary>
        /// Decode data with base64
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Decode(Stream input, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Decode(input, ms, bufferSize);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decode data with base64
        /// </summary>
        /// <param name="base64String">Data to decode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Decode(string base64String, int bufferSize = 4096)
        {
            byte[] base64Data = Encoding.ASCII.GetBytes(base64String);
            using (MemoryStream ms = new MemoryStream(base64Data))
            {
                return Decode(ms, bufferSize);
            }
        }

        /// <summary>
        /// Decode data with base64
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decode(string inputFile, string outputFile, int bufferSize = 4096)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    Decode(input, output, bufferSize);
                }
            }
        }
    }
}
