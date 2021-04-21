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

namespace DotNetToolBox.Cryptography
{
    public static class SHA3
    {

        public static byte[] Hash(byte[] data, int bitLength = 512)
        {
            Sha3Digest sha3 = new Sha3Digest(bitLength);
            sha3.BlockUpdate(data, 0, data.Length);
            byte[] result = new byte[bitLength / 8];
            sha3.DoFinal(result, 0);
            return result;
        }
    }
}
