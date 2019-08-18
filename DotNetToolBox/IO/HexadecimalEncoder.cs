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
using System.Text;

namespace DotNetToolBox.IO
{
    public static class HexadecimalEncoder
    {
        /// <summary>
        /// Encode data with Hexadecimal
        /// </summary>
        /// <param name="bytes">Data to encode</param>
        /// <param name="bytesRead">Number of bytes read</param>
        private static string InternalEncode(byte[] bytes, int bytesRead)
        {
            char[] c = new char[bytesRead * 2];
            int b;
            for (int i = 0; i < bytesRead; i++)
            {
                b = bytes[i] >> 4;
                c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }
            return new string(c);
        }

        /// <summary>
        /// Decode data with Hexadecimal
        /// </summary>
        /// <param name="hex">Data to decode</param>
        private static byte[] InternalDecode(string hex)
        {
            int nbChars = hex.Length / 2;
            byte[] bytes = new byte[nbChars];
            using (StringReader sr = new StringReader(hex))
            {
                for (int i = 0; i < nbChars; i++)
                    bytes[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return bytes;
        }

        /// <summary>
        /// Encode data with Hexadecimal
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="output">Output Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Encode(Stream input, Stream output, int bufferSize = 4096)
        {
            byte[] buffer = new byte[bufferSize];
            byte[] hexData;
            string hexStr;
            int bytesRead;
            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead > 0)
                {
                    hexStr = InternalEncode(buffer, bytesRead);
                    hexData = Encoding.ASCII.GetBytes(hexStr);
                    output.Write(hexData, 0, hexData.Length);
                }
            } while (bytesRead == bufferSize);
        }

        /// <summary>
        /// Encode data with Hexadecimal
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
        /// Encode data with Hexadecimal
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
        /// Encode data with Hexadecimal
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
        /// Decode data with Hexadecimal
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="output">Output Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decode(Stream input, Stream output, int bufferSize = 4096)
        {
            byte[] buffer = new byte[bufferSize];
            byte[] hexData;
            string hexStr;
            int bytesRead;
            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead > 0)
                {
                    hexStr = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    hexData = InternalDecode(hexStr);
                    output.Write(hexData, 0, hexData.Length);
                }
            } while (bytesRead == bufferSize);
        }

        /// <summary>
        /// Decode data with Hexadecimal
        /// </summary>
        /// <param name="input">Input Stream</param>
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
        /// Decode data with Hexadecimal
        /// </summary>
        /// <param name="hexString">Data to decode</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Decode(string hexString, int bufferSize = 4096)
        {
            byte[] hexData = Encoding.ASCII.GetBytes(hexString);
            using (MemoryStream ms = new MemoryStream(hexData))
            {
                return Decode(ms, bufferSize);
            }
        }

        /// <summary>
        /// Decode data with Hexadecimal
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
