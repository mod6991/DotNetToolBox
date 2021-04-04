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

using Org.BouncyCastle.Crypto.Digests;
using System.IO;

namespace DotNetToolBox.Cryptography
{
    public static class SHA3
    {
        /// <summary>
        /// Compute the MD5 hash value
        /// </summary>
        /// <param name="input">Input Stream</param>
        public static byte[] Hash(Stream input, int bitLength = 512)
        {
            Sha3Digest sha3 = new Sha3Digest(bitLength);
            int bytesRead;
            byte[] buffer = new byte[4096];

            do
            {
                bytesRead = input.Read(buffer, 0, 4096);
                if (bytesRead > 0)
                    sha3.BlockUpdate(buffer, 0, bytesRead);
            } while (bytesRead == 4096);

            byte[] result = new byte[bitLength / 8];
            sha3.DoFinal(result, 0);

            return result;
        }
    }
}
