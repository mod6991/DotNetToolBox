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
using System.Security.Cryptography;

namespace DotNetToolBox.Cryptography
{
    public static class SHA1
    {
        public static byte[] Hash(byte[] data)
        {
            Sha1Digest sha1 = new Sha1Digest();
            sha1.BlockUpdate(data, 0, data.Length);
            byte[] result = new byte[20];
            sha1.DoFinal(result, 0);
            return result;
        }
    }
}
