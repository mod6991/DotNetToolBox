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
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DotNetToolBox.Utils
{
    public static class RandomHelper
    {
        /// <summary>
        /// Password type enumeration
        /// </summary>
        public enum PasswordType : byte
        {
            LowerChars = 0x01,
            UpperChars = 0x02,
            Numbers = 0x04,
            SpecialChars = 0x08,
            LowerUpper = LowerChars | UpperChars,
            LowerUpperNumber = LowerUpper | Numbers,
            All = LowerUpperNumber | SpecialChars
        }

        private static char[] m_lowerChars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static char[] m_upperChars = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static char[] m_numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static char[] m_specialChars = new char[] {' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' };

        /// <summary>
        /// Generate a random password
        /// </summary>
        /// <param name="type">Password type</param>
        /// <param name="size">Password size</param>
        public static string GeneratePassword(PasswordType type, int size)
        {
            char[] chars = new char[0];
            byte total = (byte)type;

            if ((total & (byte)PasswordType.LowerChars) == (byte)PasswordType.LowerChars)
                chars = chars.Concat(m_lowerChars).ToArray();
            if ((total & (byte)PasswordType.UpperChars) == (byte)PasswordType.UpperChars)
                chars = chars.Concat(m_upperChars).ToArray();
            if ((total & (byte)PasswordType.Numbers) == (byte)PasswordType.Numbers)
                chars = chars.Concat(m_numbers).ToArray();
            if ((total & (byte)PasswordType.SpecialChars) == (byte)PasswordType.SpecialChars)
                chars = chars.Concat(m_specialChars).ToArray();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < size; i++)
                sb.Append(chars[GenerateInt32() % chars.Length]);

            return sb.ToString();
        }

        /// <summary>
        /// Generate a random password with given chars
        /// </summary>
        /// <param name="chars">Password chars</param>
        /// <param name="size">Password size</param>
        public static string GeneratePassword(char[] chars, int size)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < size; i++)
                sb.Append(chars[GenerateInt32() % chars.Length]);

            return sb.ToString();
        }

        /// <summary>
        /// Generate a byte array filled with random bytes
        /// </summary>
        /// <param name="size">Array size</param>
        public static byte[] GenerateBytes(int size)
        {
            byte[] bytes = new byte[size];
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// Generate a random 16-bits integer
        /// </summary>
        /// <param name="max">Max value</param>
        /// <param name="positiveOnly">Return positive Int16 value only</param>
        public static Int16 GenerateInt16(Int16 max = Int16.MaxValue, bool positiveOnly = true)
        {
            byte[] bytes = new byte[sizeof(Int16)];
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }

            Int16 val = BitConverter.ToInt16(bytes, 0);
            if (positiveOnly)
                val = val < 0 ? (Int16)(val * -1) : val;

            return (Int16)(val % max);
        }

        /// <summary>
        /// Generate a random 32-bits integer
        /// </summary>
        /// <param name="max">Max value</param>
        /// <param name="positiveOnly">Return positive Int32 value only</param>
        public static Int32 GenerateInt32(Int32 max = Int32.MaxValue, bool positiveOnly = true)
        {
            byte[] bytes = new byte[sizeof(Int32)];
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }

            Int32 val = BitConverter.ToInt32(bytes, 0);
            if (positiveOnly)
                val = val < 0 ? val * -1 : val;

            return val % max;
        }

        /// <summary>
        /// Generate a random 64-bits integer
        /// </summary>
        /// <param name="max">Max value</param>
        /// <param name="positiveOnly">Return positive Int64 value only</param>
        public static Int64 GenerateInt64(Int64 max = Int64.MaxValue, bool positiveOnly = true)
        {
            byte[] bytes = new byte[sizeof(Int64)];
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }

            Int64 val = BitConverter.ToInt64(bytes, 0);
            if (positiveOnly)
                val = val < 0 ? val * -1 : val;

            return val % max;
        }

        /// <summary>
        /// Generate a random double-precision floating-point number
        /// </summary>
        /// <param name="max">Max value</param>
        public static double GenerateDouble(double max = 1)
        {
            byte[] bytes = new byte[sizeof(Int32)];
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }

            int val = BitConverter.ToInt32(bytes, 0);
            return new System.Random(val).NextDouble() * max;
        }

        /// <summary>
        /// Generate a random decimal number
        /// </summary>
        /// <param name="max">Max value</param>
        public static decimal GenerateDecimal(decimal max = 1)
        {
            byte[] bytes = new byte[sizeof(Int32)];
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }

            int val = BitConverter.ToInt32(bytes, 0);
            return (decimal)new System.Random(val).NextDouble() * max;
        }

        /// <summary>
        /// Generate a random single-precision floating-point number
        /// </summary>
        /// <param name="max">Max value</param>
        public static float GenerateFloat(float max = 1)
        {
            byte[] bytes = new byte[sizeof(Int32)];
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }

            int val = BitConverter.ToInt32(bytes, 0);
            return (float)new System.Random(val).NextDouble() * max;
        }
    }
}
