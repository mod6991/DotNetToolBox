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
using System.Security.Cryptography;

namespace DotNetToolBox.Utils
{
    public static class RandomHelper
    {
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
