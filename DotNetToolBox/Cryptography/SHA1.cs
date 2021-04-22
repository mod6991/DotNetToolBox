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
    public static class SHA1
    {
        /// <summary>
        /// Hash data with SHA1
        /// </summary>
        /// <param name="data">Data to hash</param>
        /// <returns>Hash</returns>
        public static byte[] Hash(byte[] data)
        {
            byte[] result = new byte[20];

            Sha1Digest sha1 = new Sha1Digest();
            sha1.BlockUpdate(data, 0, data.Length);
            sha1.DoFinal(result, 0);

            return result;
        }

        /// <summary>
        /// Hash stream with SHA1
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <returns>Hash</returns>
        public static byte[] Hash(Stream input, int bufferSize = 4096)
        {
            byte[] result = new byte[20];

            Sha1Digest sha1 = new Sha1Digest();
            int bytesRead;
            byte[] buffer = new byte[bufferSize];

            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if(bytesRead > 0)
                    sha1.BlockUpdate(buffer, 0, bytesRead);
            } while (bytesRead == bufferSize);


            sha1.DoFinal(result, 0);

            return result;
        }
    }
}
