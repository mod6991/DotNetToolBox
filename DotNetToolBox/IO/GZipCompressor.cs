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
using System.IO.Compression;

namespace DotNetToolBox.IO
{
    public static class GZipCompressor
    {
        /// <summary>
        /// Compress data with GZip
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="output">Output Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Compress(Stream input, Stream output, int bufferSize = 4096)
        {
            using (GZipStream compress = new GZipStream(output, CompressionMode.Compress))
            {
                StreamHelper.WriteStream(input, compress, bufferSize);
            }
        }

        /// <summary>
        /// Compress data with GZip
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Compress(Stream input, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Compress(input, ms, bufferSize);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Compress data with GZip
        /// </summary>
        /// <param name="inputData">Data to compress</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Compress(byte[] inputData, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream(inputData))
            {
                return Compress(ms, bufferSize);
            }
        }

        /// <summary>
        /// Compress data with GZip
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Compress(string inputFile, string outputFile, int bufferSize = 4096)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    Compress(input, output, bufferSize);
                }
            }
        }

        /// <summary>
        /// Decompress data with GZip
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="output">Output Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decompress(Stream input, Stream output, int bufferSize = 4096)
        {
            using (GZipStream compress = new GZipStream(input, CompressionMode.Decompress))
            {
                StreamHelper.WriteStream(compress, output, bufferSize);
            }
        }

        /// <summary>
        /// Decompress data with GZip
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Decompress(Stream input, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Decompress(input, ms, bufferSize);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decompress data with GZip
        /// </summary>
        /// <param name="inputData">Data to decompress</param>
        /// <param name="bufferSize">Buffer size</param>
        public static byte[] Decompress(byte[] inputData, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream(inputData))
            {
                return Decompress(ms, bufferSize);
            }
        }

        /// <summary>
        /// Decompress data with GZip
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decompress(string inputFile, string outputFile, int bufferSize = 4096)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    Decompress(input, output, bufferSize);
                }
            }
        }
    }
}
