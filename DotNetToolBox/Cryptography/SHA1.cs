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

using System.IO;
using System.Security.Cryptography;

namespace DotNetToolBox.Cryptography
{
    public static class SHA1
    {
        /// <summary>
        /// Compute the SHA1 hash value
        /// </summary>
        /// <param name="input">Input Stream</param>
        public static byte[] Hash(Stream input)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                return sha1.ComputeHash(input);
            }
        }
    }
}
