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
        /// Hash data with SHA3
        /// </summary>
        /// <param name="data">Data to hash</param>
        /// <param name="bitLength">Size in bits</param>
        /// <returns>Hash</returns>
        public static byte[] Hash(byte[] data, int bitLength = 512)
        {
            byte[] result = new byte[bitLength / 8];

            Sha3Digest sha3 = new Sha3Digest(bitLength);
            sha3.BlockUpdate(data, 0, data.Length);
            sha3.DoFinal(result, 0);

            return result;
        }

        /// <summary>
        /// Hash stream with SHA3
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="bitLength">Size in bits</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <returns>Hash</returns>
        public static byte[] Hash(Stream input, int bitLength = 512, int bufferSize = 4096)
        {
            byte[] result = new byte[bitLength / 8];

            Sha3Digest sha3 = new Sha3Digest(bitLength);
            int bytesRead;
            byte[] buffer = new byte[bufferSize];

            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if(bytesRead > 0)
                    sha3.BlockUpdate(buffer, 0, bytesRead);
            } while (bytesRead == bufferSize);


            sha3.DoFinal(result, 0);

            return result;
        }
    }
}
