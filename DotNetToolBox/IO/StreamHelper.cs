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

using System.Text;
using System.IO;

namespace DotNetToolBox.IO
{
    public static class StreamHelper
    {
        /// <summary>
        /// Read an input Stream and write it into an output Stream
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="output">Output Stream</param>
        public static void WriteStream(Stream input, Stream output, int bufferSize = 4096)
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead > 0)
                    output.Write(buffer, 0, bytesRead);
            } while (bytesRead == bufferSize);
        }

        /// <summary>
        /// Read bytes from Stream
        /// </summary>
        /// <param name="input">Input Stream</param>
        public static byte[] ReadBytes(Stream input, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                WriteStream(input, ms, bufferSize);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Read bytes from file
        /// </summary>
        /// <param name="file">Input file</param>
        public static byte[] ReadBytes(string file, int bufferSize = 4096)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return ReadBytes(fs, bufferSize);
            }
        }

        /// <summary>
        /// Write bytes to stream
        /// </summary>
        /// <param name="data">Input data</param>
        /// <param name="output">Output Stream</param>
        public static void WriteBytes(byte[] data, Stream output, int bufferSize = 4096)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                WriteStream(ms, output, bufferSize);
            }
        }

        /// <summary>
        /// Write bytes to file
        /// </summary>
        /// <param name="data">Input data</param>
        /// <param name="file">Output file</param>
        public static void WriteBytes(byte[] data, string file, int bufferSize = 4096)
        {
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                WriteBytes(data, fs, bufferSize);
            }
        }

        /// <summary>
        /// Read String from Stream
        /// </summary>
        /// <param name="input">Input Stream</param>
        /// <param name="encoding">String encoding</param>
        public static string ReadString(Stream input, Encoding encoding = null, int bufferSize = 4096)
        {
            encoding = encoding ?? Encoding.Default;
            byte[] data = ReadBytes(input, bufferSize);
            return encoding.GetString(data);
        }

        /// <summary>
        /// Read String from file
        /// </summary>
        /// <param name="file">Input file</param>
        /// <param name="encoding">String encoding</param>
        public static string ReadString(string file, Encoding encoding = null, int bufferSize = 4096)
        {
            encoding = encoding ?? Encoding.Default;
            byte[] data = ReadBytes(file, bufferSize);
            return encoding.GetString(data);
        }

        /// <summary>
        /// Write String to Stream
        /// </summary>
        /// <param name="str">Input String</param>
        /// <param name="output">Output Stream</param>
        /// <param name="encoding">String Encoding</param>
        public static void WriteString(string str, Stream output, Encoding encoding = null, int bufferSize = 4096)
        {
            encoding = encoding ?? Encoding.Default;
            byte[] data = encoding.GetBytes(str);
            WriteBytes(data, output, bufferSize);
        }

        /// <summary>
        /// Write String to file
        /// </summary>
        /// <param name="str">Input String</param>
        /// <param name="file">Output file</param>
        /// <param name="encoding">String encoding</param>
        public static void WriteString(string str, string file, Encoding encoding = null, int bufferSize = 4096)
        {
            encoding = encoding ?? Encoding.Default;
            byte[] data = encoding.GetBytes(str);
            WriteBytes(data, file, bufferSize);
        }

        /// <summary>
        /// Get a new filestream in create mode
        /// </summary>
        /// <param name="file">File path</param>
        public static FileStream GetFileStreamCreate(string file)
        {
            return new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write);
        }

        /// <summary>
        /// Get a new filestream in open mode
        /// </summary>
        /// <param name="file">File path</param>
        public static FileStream GetFileStreamOpen(string file)
        {
            return new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>
        /// Get a new filestream in append mode
        /// </summary>
        /// <param name="file">File path</param>
        public static FileStream GetFileStreamAppend(string file)
        {
            return new FileStream(file, FileMode.Append, FileAccess.Write, FileShare.Write);
        }
    }
}
