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

namespace DotNetToolBox.Cryptography
{
    public static class SHA512Hasher
    {
        /// <summary>
        /// Compute the SHA512 hash value
        /// </summary>
        /// <param name="input">Input Stream</param>
        public static byte[] Hash(Stream input)
        {
            using (SHA512CryptoServiceProvider sha512 = new SHA512CryptoServiceProvider())
            {
                return sha512.ComputeHash(input);
            }
        }

        /// <summary>
        /// Compute the SHA512 hash value
        /// </summary>
        /// <param name="data">Data to hash</param>
        public static byte[] Hash(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Hash(ms);
            }
        }

        /// <summary>
        /// Compute the SHA512 hash value
        /// </summary>
        /// <param name="file">Input file</param>
        public static byte[] Hash(string file)
        {
            using (FileStream input = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Hash(input);
            }
        }
    }
}
