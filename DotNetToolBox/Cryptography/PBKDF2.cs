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

using System.Security.Cryptography;

namespace DotNetToolBox.Cryptography
{
    public static class PBKDF2
    {
        /// <summary>
        /// Generate a pseudo-random key based on a password and salt
        /// </summary>
        /// <param name="nbBytes">Number of bytes to generate</param>
        /// <param name="password">Password</param>
        /// <param name="salt">Salt</param>
        /// <param name="iterations">Iterations</param>
        /// <returns></returns>
        public static byte[] GenerateKeyFromPassword(int nbBytes, string password, byte[] salt, int iterations = 10000)
        {
            using (Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pdb.GetBytes(nbBytes);
            }
        }
    }
}
