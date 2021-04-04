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

using DotNetToolBox.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNetToolBox.Cryptography
{
    public static class RSA
    {
        /// <summary>
        /// Generate a new RSA key pair
        /// </summary>
        /// <param name="keySize">Key size in bits</param>
        public static RSACryptoServiceProvider GenerateKeyPair(int keySize = 4096)
        {
            return new RSACryptoServiceProvider(keySize);
        }

        #region Encrypt / Decrypt data

        /// <summary>
        /// Encrypt data using RSACryptoServiceProvider
        /// </summary>
        /// <param name="rsa">RSA Key</param>
        /// <param name="data">Data to encrypt</param>
        public static byte[] Encrypt(RSACryptoServiceProvider rsa, byte[] data)
        {
            return rsa.Encrypt(data, true);
        }

        /// <summary>
        /// Decrypt data using RSACryptoServiceProvider
        /// </summary>
        /// <param name="rsa">RSA Key</param>
        /// <param name="encrypted">Data to decrypt</param>
        public static byte[] Decrypt(RSACryptoServiceProvider rsa, byte[] encrypted)
        {
            return rsa.Decrypt(encrypted, true);
        }

        #endregion

        #region Save / Load PEM files

        /// <summary>
        ///Load a RSA key from a PEM file 
        /// </summary>
        /// <param name="file">PEM file</param>
        /// <param name="password">Password</param>
        public static RSACryptoServiceProvider LoadFromPEM(Stream input, string password = null)
        {
            using (StreamReader sr = new StreamReader(input, Encoding.Default))
            {
                PemReader pemReader;
                if (!string.IsNullOrEmpty(password))
                    pemReader = new PemReader(sr, new PasswordFinder(password));
                else
                    pemReader = new PemReader(sr);

                RSAParameters parameters;
                object obj = pemReader.ReadObject();

                if (obj == null)
                    throw new PemException("PemReader.ReadObject() returned null");

                Type objType = obj.GetType();

                if (objType == typeof(AsymmetricCipherKeyPair))
                {
                    AsymmetricCipherKeyPair ackp = (AsymmetricCipherKeyPair)obj;
                    parameters = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)ackp.Private);
                }
                else if (objType == typeof(RsaPrivateCrtKeyParameters))
                    parameters = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)obj);
                else if (objType == typeof(RsaKeyParameters))
                    parameters = DotNetUtilities.ToRSAParameters((RsaKeyParameters)obj);
                else
                    throw new PemException($"Cannot handle type '{objType}' returned by PemReader.ReadObject()");

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(parameters);
                return rsa;
            }
        }

        /// <summary>
        /// Save a public RSA key to a PEM file
        /// </summary>
        /// <param name="rsa">Public key</param>
        /// <param name="file">PEM file</param>
        public static void SavePublicKeyToPEM(RSACryptoServiceProvider rsa, Stream output)
        {
            using (StreamWriter sw = new StreamWriter(output, Encoding.Default))
            {
                PemWriter pemWriter = new PemWriter(sw);
                RsaKeyParameters rkp = DotNetUtilities.GetRsaPublicKey(rsa);
                pemWriter.WriteObject(rkp);
            }
        }

        /// <summary>
        /// Save an encrypted private RSA key to a PEM file
        /// </summary>
        /// <param name="rsa">Private key</param>
        /// <param name="file">PEM file</param>
        /// <param name="password">Password</param>
        /// <param name="algorithm">Algorithm for PEM encryption</param>
        public static void SavePrivateKeyToPEM(RSACryptoServiceProvider rsa, Stream output, string password, string algorithm = "AES-256-CBC")
        {
            using (StreamWriter sw = new StreamWriter(output, Encoding.Default))
            {
                PemWriter pemWriter = new PemWriter(sw);
                AsymmetricCipherKeyPair ackp = DotNetUtilities.GetRsaKeyPair(rsa);
                RsaPrivateCrtKeyParameters privKey = (RsaPrivateCrtKeyParameters)ackp.Private;
                pemWriter.WriteObject(privKey, algorithm, password.ToCharArray(), new SecureRandom());
            }
        }

        /// <summary>
        /// Save a private RSA key to a PEM file
        /// </summary>
        /// <param name="rsa">Private key</param>
        /// <param name="file">PEM file</param>
        public static void SavePrivateKeyToPEM(RSACryptoServiceProvider rsa, Stream output)
        {
            using (StreamWriter sw = new StreamWriter(output, Encoding.Default))
            {
                PemWriter pemWriter = new PemWriter(sw);
                AsymmetricCipherKeyPair ackp = DotNetUtilities.GetRsaKeyPair(rsa);
                RsaPrivateCrtKeyParameters privKey = (RsaPrivateCrtKeyParameters)ackp.Private;
                pemWriter.WriteObject(privKey);
            }
        }

        #endregion

        #region Save / Load Win KeyStore

        /// <summary>
        /// Save a RSA key in the Windows KeyStore
        /// </summary>
        /// <param name="rsa">RSA key</param>
        /// <param name="ContainerName">Container name</param>
        /// <param name="csppf">CspProviderFlags</param>
        public static void SaveInWinKeyStore(RSACryptoServiceProvider rsa, string ContainerName, CspProviderFlags csppf = CspProviderFlags.UseMachineKeyStore)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;
            cp.Flags = csppf;

            using (RSACryptoServiceProvider winKSkey = new RSACryptoServiceProvider(cp))
            {
                winKSkey.FromXmlString(rsa.ToXmlString(true));
            }
        }

        /// <summary>
        /// Load a RSA key from the Windows KeyStore 
        /// </summary>
        /// <param name="ContainerName">Container name</param>
        /// <param name="csppf">CspProviderFlags</param>
        public static RSACryptoServiceProvider LoadFromWinKeyStore(string ContainerName, CspProviderFlags csppf = CspProviderFlags.UseMachineKeyStore)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;
            cp.Flags = csppf;

            return new RSACryptoServiceProvider(cp);
        }

        /// <summary>
        /// Delete a RSA key from the Windows KeyStore
        /// </summary>
        /// <param name="ContainerName">Container name</param>
        /// <param name="csppf">CspProviderFlags</param>
        public static void DeleteFromWinKeyStore(string ContainerName, CspProviderFlags csppf = CspProviderFlags.UseMachineKeyStore)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;
            cp.Flags = csppf;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp))
            {
                rsa.PersistKeyInCsp = false;
                rsa.Clear();
            }
        }

        #endregion

        class PasswordFinder : IPasswordFinder
        {
            private string _password;

            public PasswordFinder(string password)
            {
                _password = password;
            }

            public char[] GetPassword()
            {
                return _password.ToCharArray();
            }
        }

        public enum EncodingType
        {
            Base64 = 0x01,
            Hexadecimal = 0x02
        }
    }
}
