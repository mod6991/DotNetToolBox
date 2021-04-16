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
using System.Text;

namespace DotNetToolBox.IO
{
    public class HexDecodeException : Exception
    {
        public HexDecodeException(string message) : base(message) { }
    }

    public static class Hex
    {
        /// <summary>
        /// Encode bytes to hex string
        /// </summary>
        /// <param name="data">Byte array</param>
        /// <returns>Hex string</returns>
        public static string Encode(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            char[] ca = new char[data.Length * 2];
            byte b;

            for (int i = 0; i < data.Length; i++)
            {
                b = (byte)(data[i] >> 4);
                ca[i * 2] = (char)(b < 0x0a ? b | 0x30 : ((b - 1) & 0x07) | 0x60);
                b = (byte)(data[i] & 0x0F);
                ca[i * 2 + 1] = (char)(b < 0x0a ? b | 0x30 : ((b - 1) & 0x07) | 0x60);
            }

            return new string(ca);
        }

        /// <summary>
        /// Decode hex string to bytes
        /// </summary>
        /// <param name="str">Hex string</param>
        /// <returns>Byte array</returns>
        public static byte[] Decode(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (str.Length % 2 != 0)
                throw new HexDecodeException("Invalid input string length (not a multiple of 2)");

            byte[] data = new byte[str.Length / 2];
            char c1, c2;
            int b1, b2;

            for (int i = 0; i < data.Length; i++)
            {
                c1 = str[i * 2];
                c2 = str[i * 2 + 1];

                if (!((c1 >= 0x30 && c1 <= 0x39) || (c1 >= 0x61 && c1 <= 0x66) || (c1 >= 0x41 && c1 <= 0x46)))
                    throw new HexDecodeException($"Invalid hex char '{c1}'");

                if (!((c2 >= 0x30 && c2 <= 0x39) || (c2 >= 0x61 && c2 <= 0x66) || (c2 >= 0x41 && c2 <= 0x46)))
                    throw new HexDecodeException($"Invalid hex char '{c2}'");

                b1 = (c1 & 0xF0) == 0x30 ? c1 & 0x0F : ((c1 & 0x0F) | 0x08) + 1;
                b2 = (c2 & 0xF0) == 0x30 ? c2 & 0x0F : ((c2 & 0x0F) | 0x08) + 1;
                data[i] = (byte)(b1 << 4 | b2);
            }

            return data;
        }
    }
}