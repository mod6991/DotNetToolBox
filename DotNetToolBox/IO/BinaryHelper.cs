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
using System.Text;

namespace DotNetToolBox.IO
{
    public static class BinaryHelper
    {
        /// <summary>
        /// Write byte to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">Byte value</param>
        public static void WriteByte(Stream stream, byte value)
        {
            stream.Write(new byte[] { value }, 0, 1);
        }

        /// <summary>
        /// Write bytes to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">Bytes value</param>
        public static void WriteBytes(Stream stream, byte[] value)
        {
            stream.Write(value, 0, value.Length);
        }

        /// <summary>
        /// Write bool to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">Bool value</param>
        public static void WriteBool(Stream stream, bool value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 1);
        }

        /// <summary>
        /// Write Int16 to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">Int16 value</param>
        public static void WriteInt16(Stream stream, Int16 value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 2);
        }

        /// <summary>
        /// Write UInt16 to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">UInt16 value</param>
        public static void WriteUInt16(Stream stream, UInt16 value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 2);
        }

        /// <summary>
        /// Write Int32 to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">Int32 value</param>
        public static void WriteInt32(Stream stream, Int32 value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 4);
        }

        /// <summary>
        /// Write UInt32 to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">UInt32 value</param>
        public static void WriteUInt32(Stream stream, UInt32 value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 4);
        }

        /// <summary>
        /// Write Int64 to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">Int64 value</param>
        public static void WriteInt64(Stream stream, Int64 value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 8);
        }

        /// <summary>
        /// Write UInt64 to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">UInt64 value</param>
        public static void WriteUInt64(Stream stream, UInt64 value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 8);
        }

        /// <summary>
        /// Write float to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">float value</param>
        public static void WriteFloat(Stream stream, float value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 4);
        }

        /// <summary>
        /// Write double to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">double value</param>
        public static void WriteDouble(Stream stream, double value)
        {
            byte[] data = BitConverter.GetBytes(value);
            stream.Write(data, 0, 8);
        }

        /// <summary>
        /// Write string to stream
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">String value</param>
        /// <param name="encoding">String encoding</param>
        public static void WriteString(Stream stream, string value, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(value);
            stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Write a Length-Value
        /// </summary>
        /// <param name="stream">Output stream</param>
        /// <param name="value">Value</param>
        public static void WriteLV(Stream stream, byte[] value)
        {
            WriteInt32(stream, value.Length);
            WriteBytes(stream, value);
        }

        /// <summary>
        /// Read byte from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static byte ReadByte(Stream stream)
        {
            byte[] buffer = new byte[1];

            if (stream.Read(buffer, 0, 1) != 1)
                throw new IOException("Incorrect number of bytes returned");

            return buffer[0];
        }

        /// <summary>
        /// Read bytes from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        /// <param name="nbBytes">Number of bytes to read</param>
        public static byte[] ReadBytes(Stream stream, int nbBytes)
        {
            byte[] buffer = new byte[nbBytes];

            if (stream.Read(buffer, 0, nbBytes) != nbBytes)
                throw new IOException("Incorrect number of bytes returned");
            
            return buffer;
        }

        /// <summary>
        /// Read bool from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static bool ReadBool(Stream stream)
        {
            byte[] buffer = new byte[1];

            if (stream.Read(buffer, 0, 1) != 1)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToBoolean(buffer, 0);
        }

        /// <summary>
        /// Read Int16 from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static Int16 ReadInt16(Stream stream)
        {
            byte[] buffer = new byte[2];

            if (stream.Read(buffer, 0, 2) != 2)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Read UInt16 from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static UInt16 ReadUInt16(Stream stream)
        {
            byte[] buffer = new byte[2];

            if (stream.Read(buffer, 0, 2) != 2)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read Int32 from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static Int32 ReadInt32(Stream stream)
        {
            byte[] buffer = new byte[4];

            if (stream.Read(buffer, 0, 4) != 4)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Read UInt32 from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static UInt32 ReadUInt32(Stream stream)
        {
            byte[] buffer = new byte[4];

            if (stream.Read(buffer, 0, 4) != 4)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read Int64 from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static Int64 ReadInt64(Stream stream)
        {
            byte[] buffer = new byte[8];

            if (stream.Read(buffer, 0, 8) != 8)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Read UInt64 from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static UInt64 ReadUInt64(Stream stream)
        {
            byte[] buffer = new byte[8];

            if (stream.Read(buffer, 0, 8) != 8)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read float from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static float ReadFloat(Stream stream)
        {
            byte[] buffer = new byte[4];

            if (stream.Read(buffer, 0, 4) != 4)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Read double from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static double ReadDouble(Stream stream)
        {
            byte[] buffer = new byte[8];

            if (stream.Read(buffer, 0, 8) != 8)
                throw new IOException("Incorrect number of bytes returned");

            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Read a Length-Value
        /// </summary>
        /// <param name="stream">Input stream</param>
        public static byte[] ReadLV(Stream stream)
        {
            int valueLength = ReadInt32(stream);
            return ReadBytes(stream, valueLength);
        }
    }
}
