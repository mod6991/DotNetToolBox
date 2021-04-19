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

namespace DotNetToolBox.IO
{
    public class Base64DecodeException : Exception
    {
        public Base64DecodeException(string message) : base(message) { }
    }

    public static class Base64
    {
        internal static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        internal static char padding = '=';

        /// <summary>
        /// Encode bytes to Base64 string
        /// </summary>
        /// <param name="data">Byte array</param>
        /// <returns>Base64 string</returns>
        public static string Encode(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            char[] ca = new char[(data.Length / 3 + 1) * 4];
            int loops = data.Length / 3;
            int mod = data.Length % 3;
            int i;
            byte b1, b2, b3;

            for (i = 0; i < loops; i++)
            {
                b1 = data[i * 3];
                b2 = data[i * 3 + 1];
                b3 = data[i * 3 + 2];
                ca[i * 4] = chars[b1 >> 2];
                ca[i * 4 + 1] = chars[(b1 & 0x03) << 4 | b2 >> 4];
                ca[i * 4 + 2] = chars[(b2 & 0x0f) << 2 | b3 >> 6];
                ca[i * 4 + 3] = chars[b3 & 0x3f];
            }

            if (mod == 2)
            {
                b1 = data[i * 3];
                b2 = data[i * 3 + 1];
                ca[i * 4] = chars[b1 >> 2];
                ca[i * 4 + 1] = chars[(b1 & 0x03) << 4 | b2 >> 4];
                ca[i * 4 + 2] = chars[(b2 & 0x0f) << 2];
                ca[i * 4 + 3] = padding;
            }
            else if (mod == 1)
            {
                b1 = data[i * 3];
                ca[i * 4] = chars[b1 >> 2];
                ca[i * 4 + 1] = chars[(b1 & 0x03) << 4];
                ca[i * 4 + 2] = padding;
                ca[i * 4 + 3] = padding;
            }

            return new string(ca);
        }

        /// <summary>
        /// Decode Base64 string to bytes
        /// </summary>
        /// <param name="str">Base64 string</param>
        /// <returns>Byte array</returns>
        public static byte[] Decode(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (str.Length % 4 != 0)
                throw new Base64DecodeException("Invalid input string length (not a multiple of 4)");

            byte[] data = new byte[(str.Length / 4) * 3];
            int loops = str.Length / 4;
            int i;
            int b1, b2, b3, b4;

            for (i = 0; i < loops; i++)
            {
                b1 = chars.IndexOf(str[i * 4]);
                if (b1 == -1)
                    throw new Base64DecodeException($"Invalid base64 char1 '{str[i * 4]}'");

                b2 = chars.IndexOf(str[i * 4 + 1]);
                if (b2 == -1)
                    throw new Base64DecodeException($"Invalid base64 char2 '{str[i * 4 + 1]}'");

                b3 = chars.IndexOf(str[i * 4 + 2]);
                if (b3 == -1 && str[i * 4 + 2] != '=')
                    throw new Base64DecodeException($"Invalid base64 char3 '{str[i * 4 + 2]}'");

                b4 = chars.IndexOf(str[i * 4 + 3]);
                if (b4 == -1 && str[i * 4 + 3] != '=')
                    throw new Base64DecodeException($"Invalid base64 char4 '{str[i * 4 + 3]}'");


                data[i * 3] = (byte)(b1 << 2 | b2 >> 4);
                data[i * 3 + 1] = (byte)((b2 & 0x0f) << 4 | b3 >> 2);
                data[i * 3 + 2] = (byte)((b3 & 0x03) << 6 | b4 & 0x3f);
            }

            return data;
        }
    }
}
