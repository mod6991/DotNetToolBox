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

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;

namespace DotNetToolBox.Cryptography
{
    public static class AES
    {
        public const int KEY_SIZE = 32;
        public const int IV_SIZE = 16;
        public const int BLOCK_SIZE = 16;

        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            IBufferedCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(new AesEngine()));

            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), iv, 0, iv.Length);
            cipher.Init(true, parameters);
            byte[] enc = new byte[data.Length];
            cipher.ProcessBytes(data, enc, 0);

            return data;
        }

        public static void Encrypt(Stream input, Stream output, byte[] key, byte[] iv)
        {
            throw new NotImplementedException();
        }

        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            IBufferedCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(new BlowfishEngine()));

            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), iv, 0, iv.Length);
            cipher.Init(false, parameters);
            byte[] dec = new byte[data.Length];
            cipher.ProcessBytes(data, dec, 0);

            return dec;
        }

        public static void Decrypt(Stream input, Stream output, byte[] key, byte[] iv)
        {
            throw new NotImplementedException();
        }
    }
}
