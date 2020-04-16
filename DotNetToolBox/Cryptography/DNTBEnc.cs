#region license

//DotNetToolBox.FileEncryptor .NET file encryptor
//Copyright (C) 2015  Josué Clément
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

using DotNetToolBox.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNetToolBox.Cryptography
{
    public static class DNTBEnc
    {
        private const string _header = "DNTBENC!";
        private const byte _version = 0x04;
        private const Int32 _sizeofInt = sizeof(int);

        #region Encrypt/Decrypt with password

        /// <summary>
        /// Encrypt with password
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="password">Password</param>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void EncryptWithPassword(Stream input, Stream output, string password, Algorithm algorithm = Algorithm.AES, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            byte[] salt, key, iv;
            salt = RandomHelper.GenerateBytes(16);

            switch (algorithm)
            {
                case Algorithm.AES:
                    AES.GenerateKeyFromPassword(password, salt, out key);
                    AES.GenerateIV(out iv);
                    break;
                case Algorithm.TripleDES:
                    TripleDES.GenerateKeyFromPassword(password, salt, out key);
                    TripleDES.GenerateIV(out iv);
                    break;
                default:
                    throw new InvalidOperationException("Invalid algorithm");
            }

            InternalEncryptWithPassword(input, output, salt, key, iv, algorithm, bufferSize, notifyProgression);
        }

        /// <summary>
        /// Encrypt with password
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="password">Password</param>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void EncryptWithPassword(string inputFile, string outputFile, string password, Algorithm algorithm = Algorithm.AES, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    EncryptWithPassword(fsInput, fsOutput, password, algorithm, bufferSize, notifyProgression);
                }
            }
        }

        /// <summary>
        /// Encrypt with password
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="salt">Salt</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        private static void InternalEncryptWithPassword(Stream input, Stream output, byte[] salt, byte[] key, byte[] iv, Algorithm algorithm = Algorithm.AES, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            //Write header
            output.Write(Encoding.ASCII.GetBytes(_header), 0, 8);
            //Write version
            output.Write(new byte[] { _version }, 0, 1);
            //Write encryption type
            output.Write(new byte[] { (byte)EncryptionType.Password }, 0, 1);
            //Write algorithm
            output.Write(new byte[] { (byte)algorithm }, 0, 1);
            //Write salt length
            output.Write(BitConverter.GetBytes(salt.Length), 0, _sizeofInt);
            //Write salt
            output.Write(salt, 0, salt.Length);
            //Write iv length
            output.Write(BitConverter.GetBytes(iv.Length), 0, _sizeofInt);
            //Write iv
            output.Write(iv, 0, iv.Length);

            switch (algorithm)
            {
                case Algorithm.AES:
                    AES.Encrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, bufferSize, notifyProgression);
                    break;
                case Algorithm.TripleDES:
                    TripleDES.Encrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, bufferSize, notifyProgression);
                    break;
                default:
                    throw new InvalidOperationException("Invalid algorithm");
            }
        }

        /// <summary>
        /// Decrypt with password
        /// </summary>
        /// <param name="input">input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="password">Password</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void DecryptWithPassword(Stream input, Stream output, string password, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            byte[] buffer = new byte[8];
            int read = 0;

            //Read header
            read = input.Read(buffer, 0, 8);
            if (read != 8 || Encoding.ASCII.GetString(buffer) != _header)
                throw new InvalidOperationException("The file is not encrypted with DotNetToolBox.FileEncryptor");

            //Read version
            buffer = new byte[1];
            read = input.Read(buffer, 0, 1);
            if (read != 1 || buffer[0] != _version)
                throw new InvalidOperationException("Invalid version");

            //Read encryption type
            read = input.Read(buffer, 0, 1);
            EncryptionType encryptionType = (EncryptionType)Enum.Parse(typeof(EncryptionType), buffer[0].ToString());
            if (read != 1 || encryptionType != EncryptionType.Password)
                throw new InvalidOperationException("The file has not been encrypted with a password");

            //Read algorithm
            read = input.Read(buffer, 0, 1);
            Algorithm algorithm = (Algorithm)Enum.Parse(typeof(Algorithm), buffer[0].ToString());
            if (read != 1 || (algorithm != Algorithm.AES && algorithm != Algorithm.TripleDES))
                throw new InvalidOperationException("Invalid algorithm");

            //Read salt length
            buffer = new byte[_sizeofInt];
            read = input.Read(buffer, 0, _sizeofInt);
            if (read != _sizeofInt)
                throw new InvalidOperationException("Cannot read salt length");
            int saltLength = BitConverter.ToInt32(buffer, 0);

            //Read salt
            byte[] salt = new byte[saltLength];
            read = input.Read(salt, 0, saltLength);
            if (read != saltLength)
                throw new InvalidOperationException("Cannot read salt");

            //Read IV length
            buffer = new byte[_sizeofInt];
            read = input.Read(buffer, 0, _sizeofInt);
            if (read != _sizeofInt)
                throw new InvalidOperationException("Cannot read iv length");
            int ivLength = BitConverter.ToInt32(buffer, 0);

            //Read IV
            byte[] iv = new byte[ivLength];
            read = input.Read(iv, 0, ivLength);
            if (read != ivLength)
                throw new InvalidOperationException("Cannot read iv");

            if (notifyProgression != null)
                notifyProgression(11 + (2 * _sizeofInt) + saltLength + ivLength);

            byte[] key;

            switch (algorithm)
            {
                case Algorithm.AES:
                    AES.GenerateKeyFromPassword(password, salt, out key);
                    AES.Decrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, bufferSize, notifyProgression);
                    break;
                case Algorithm.TripleDES:
                    TripleDES.GenerateKeyFromPassword(password, salt, out key);
                    TripleDES.Decrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, bufferSize, notifyProgression);
                    break;
                default:
                    throw new InvalidOperationException("Invalid algorithm");
            }
        }

        /// <summary>
        /// Decrypt with password
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="password">Password</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void DecryptWithPassword(string inputFile, string outputFile, string password, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    DecryptWithPassword(fsInput, fsOutput, password, bufferSize, notifyProgression);
                }
            }
        }

        #endregion

        #region Encrypt/Decrypt with RSA

        /// <summary>
        /// Encrypt with RSA
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="keyName">Key name</param>
        /// <param name="keyType">Key type</param>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void EncryptWithRSA(Stream input, Stream output, RSACryptoServiceProvider rsa, string keyName, KeyType keyType, Algorithm algorithm = Algorithm.AES, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            //Write header
            output.Write(Encoding.ASCII.GetBytes(_header), 0, 8);
            //Write version
            output.Write(new byte[] { _version }, 0, 1);
            //Write encryption type
            output.Write(new byte[] { (byte)EncryptionType.RSA }, 0, 1);
            //Write algorithm
            output.Write(new byte[] { (byte)algorithm }, 0, 1);
            //Write key type
            output.Write(new byte[] { (byte)keyType }, 0, 1);
            //Write key name length
            output.Write(BitConverter.GetBytes(keyName.Length), 0, _sizeofInt);
            //Write key name
            output.Write(Encoding.ASCII.GetBytes(keyName), 0, keyName.Length);

            byte[] key, iv, encKey;

            switch (algorithm)
            {
                case Algorithm.AES:
                    //Generate random AES key + IV
                    AES.GenerateKeyIV(out key, out iv);
                    //Encrypt the AES key with the RSA key
                    encKey = RSA.Encrypt(rsa, key);
                    //Write the encrypted AES key length
                    output.Write(BitConverter.GetBytes(encKey.Length), 0, _sizeofInt);
                    //Write the encrypted AES key
                    output.Write(encKey, 0, encKey.Length);
                    //Write the IV length
                    output.Write(BitConverter.GetBytes(iv.Length), 0, _sizeofInt);
                    //Write the IV
                    output.Write(iv, 0, iv.Length);
                    //Encrypt the input file with the AES key + IV
                    AES.Encrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, bufferSize, notifyProgression);
                    break;
                case Algorithm.TripleDES:
                    //Generate random TripleDES key + IV
                    TripleDES.GenerateKeyIV(out key, out iv);
                    //Encrypt the TripleDES key with the RSA key
                    encKey = RSA.Encrypt(rsa, key);
                    //Write the encrypted TripleDES key length
                    output.Write(BitConverter.GetBytes(encKey.Length), 0, _sizeofInt);
                    //Write the encrypted TripleDES key
                    output.Write(encKey, 0, encKey.Length);
                    //Write the IV length
                    output.Write(BitConverter.GetBytes(iv.Length), 0, _sizeofInt);
                    //Write the IV
                    output.Write(iv, 0, iv.Length);
                    //Encrypt the input file with the TripleDES key + IV
                    TripleDES.Encrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, bufferSize, notifyProgression);
                    break;
                default:
                    throw new InvalidOperationException("Invalid algorithm");
            }
        }

        /// <summary>
        /// Encrypt with RSA
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="keyName">Key name</param>
        /// <param name="keyType">Key type</param>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void EncryptWithRSA(string inputFile, string outputFile, RSACryptoServiceProvider rsa, string keyName, KeyType keyType, Algorithm algorithm = Algorithm.AES, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    EncryptWithRSA(fsInput, fsOutput, rsa, keyName, keyType, algorithm, bufferSize, notifyProgression);
                }
            }
        }

        /// <summary>
        /// Decrypt with RSA
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void DecryptWithRSA(Stream input, Stream output, RSACryptoServiceProvider rsa, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            byte[] buffer = new byte[8];
            int read = 0;

            //Read header
            read = input.Read(buffer, 0, 8);
            if (read != 8 || Encoding.ASCII.GetString(buffer) != _header)
                throw new InvalidOperationException("The file is not encrypted with DNTBEnc");

            //Read version
            buffer = new byte[1];
            read = input.Read(buffer, 0, 1);
            if (read != 1 || buffer[0] != _version)
                throw new InvalidOperationException("Invalid version");

            //Read encryption type
            read = input.Read(buffer, 0, 1);
            EncryptionType encryptionType = (EncryptionType)Enum.Parse(typeof(EncryptionType), buffer[0].ToString());
            if (read != 1 || encryptionType != EncryptionType.RSA)
                throw new InvalidOperationException("The file has not been encrypted with a password");

            //Read algorithm
            read = input.Read(buffer, 0, 1);
            Algorithm algorithm = (Algorithm)Enum.Parse(typeof(Algorithm), buffer[0].ToString());
            if (read != 1 || (algorithm != Algorithm.AES))
                throw new InvalidOperationException("Invalid algorithm");

            //Read key type
            read = input.Read(buffer, 0, 1);
            KeyType readKeyType = (KeyType)Enum.Parse(typeof(KeyType), buffer[0].ToString());

            //Read the key name length
            buffer = new byte[_sizeofInt];
            read = input.Read(buffer, 0, _sizeofInt);
            int keyNameLength = BitConverter.ToInt32(buffer, 0);
            //Ignore the key name by seeking the end of it
            input.Seek(keyNameLength, SeekOrigin.Current);

            //Read the encrypted symmetric key size
            buffer = new byte[_sizeofInt];
            read = input.Read(buffer, 0, _sizeofInt);
            int encKeySize = BitConverter.ToInt32(buffer, 0);

            //Read the symmetric key
            byte[] encKey = new byte[encKeySize];
            read = input.Read(encKey, 0, encKeySize);

            //Read the IV size
            buffer = new byte[_sizeofInt];
            read = input.Read(buffer, 0, _sizeofInt);
            int ivSize = BitConverter.ToInt32(buffer, 0);

            //Read the IV
            byte[] iv = new byte[ivSize];
            read = input.Read(iv, 0, ivSize);

            //Decrypt the symmetric key with the RSA key
            byte[] key = RSA.Decrypt(rsa, encKey);

            if (notifyProgression != null)
                notifyProgression(12 + (3 * _sizeofInt) + keyNameLength + encKeySize + ivSize);

            switch (algorithm)
            {
                case Algorithm.AES:
                    AES.Decrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, bufferSize, notifyProgression);
                    break;
                case Algorithm.TripleDES:
                    TripleDES.Decrypt(input, output, key, iv, CipherMode.CBC, PaddingMode.PKCS7, bufferSize, notifyProgression);
                    break;
                default:
                    throw new InvalidOperationException("Invalid algorithm");
            }
        }

        /// <summary>
        /// Decrypt with RSA
        /// </summary>
        /// <param name="inputFile">Input file</param>
        /// <param name="outputFile">Output file</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void DecryptWithRSA(string inputFile, string outputFile, RSACryptoServiceProvider rsa, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    DecryptWithRSA(fsInput, fsOutput, rsa, bufferSize, notifyProgression);
                }
            }
        }

        #endregion

        public enum Algorithm
        {
            AES = 0x01,
            TripleDES = 0x02
        }

        public enum KeyType
        {
            PEM = 0x01,
            XML = 0x02,
            WinKS = 0x03
        }

        public enum EncryptionType
        {
            Password = 0x01,
            RSA = 0x02
        }
    }
}
