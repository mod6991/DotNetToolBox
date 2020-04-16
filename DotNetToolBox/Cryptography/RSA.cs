#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012  Josué Clément
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

        #region Encrypt / Decrypt password

        /// <summary>
        /// Encrypt a password
        /// </summary>
        /// <param name="password">Password to encrypt</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="passwordEncoding">Password text encoding</param>
        /// <param name="resultEncodingType">Result encoding</param>
        public static string EncryptPassword(string password, RSACryptoServiceProvider rsa, Encoding passwordEncoding, EncodingType resultEncodingType)
        {
            byte[] passwordData = passwordEncoding.GetBytes(password);
            byte[] encryptedPassword = Encrypt(rsa, passwordData);

            switch (resultEncodingType)
            {
                case EncodingType.Base64:
                    return Base64.Encode(encryptedPassword);
                case EncodingType.Hexadecimal:
                    return Hex.Encode(encryptedPassword);
                default:
                    throw new Exception("Invalid encoding type !");
            }
        }

        /// <summary>
        /// Decrypt a password
        /// </summary>
        /// <param name="encodedEncryptedPassword">Encoded password to decrypt</param>
        /// <param name="rsa">RSA key</param>
        /// <param name="passwordEncoding">Password text encoding</param>
        /// <param name="passwordEncodingType">Password encoding type</param>
        public static string DecryptPassword(string encodedEncryptedPassword, RSACryptoServiceProvider rsa, Encoding passwordEncoding, EncodingType passwordEncodingType)
        {
            byte[] encryptedPassword;

            switch (passwordEncodingType)
            {
                case EncodingType.Base64:
                    encryptedPassword = Base64.Decode(encodedEncryptedPassword);
                    break;
                case EncodingType.Hexadecimal:
                    encryptedPassword = Hex.Decode(encodedEncryptedPassword);
                    break;
                default:
                    throw new Exception("Invalid encoding type !");
            }

            byte[] decryptedPassword = Decrypt(rsa, encryptedPassword);
            return passwordEncoding.GetString(decryptedPassword);
        }

        #endregion

        #region Save / Load PEM files

        /// <summary>
        /// Load a public key from a PEM file
        /// </summary>
        /// <param name="file">PEM file</param>
        public static RSACryptoServiceProvider LoadPublicKeyFromPEM(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    PemReader pemReader = new PemReader(sr);
                    RsaKeyParameters rkp = (RsaKeyParameters)pemReader.ReadObject();
                    RSAParameters parameters = DotNetUtilities.ToRSAParameters(rkp);
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.ImportParameters(parameters);
                    return rsa;
                }
            }
        }

        /// <summary>
        ///Load a private key from a PEM file 
        /// </summary>
        /// <param name="file">PEM file</param>
        /// <param name="password">Password</param>
        public static RSACryptoServiceProvider LoadPrivateKeyFromPEM(string file, string password)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    PemReader pemReader = new PemReader(sr, new PasswordFinder(password));
                    AsymmetricCipherKeyPair ackp = (AsymmetricCipherKeyPair)pemReader.ReadObject();
                    RSAParameters parameters = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)ackp.Private);
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.ImportParameters(parameters);
                    return rsa;
                }
            }
        }

        /// <summary>
        /// Load a private key from a PEM file
        /// </summary>
        /// <param name="file">PEM file</param>
        public static RSACryptoServiceProvider LoadPrivateKeyFromPEM(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    PemReader pemReader = new PemReader(sr);
                    AsymmetricCipherKeyPair ackp = (AsymmetricCipherKeyPair)pemReader.ReadObject();
                    RSAParameters parameters = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)ackp.Private);
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.ImportParameters(parameters);
                    return rsa;
                }
            }
        }

        /// <summary>
        /// Save a public key to a PEM file
        /// </summary>
        /// <param name="rsa">Public key</param>
        /// <param name="file">PEM file</param>
        public static void SavePublicKeyToPEM(RSACryptoServiceProvider rsa, string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    PemWriter pemWriter = new PemWriter(sw);
                    RsaKeyParameters rkp = DotNetUtilities.GetRsaPublicKey(rsa);
                    pemWriter.WriteObject(rkp);
                }
            }
        }

        /// <summary>
        /// Save a private key to a PEM file
        /// </summary>
        /// <param name="rsa">Private key</param>
        /// <param name="file">PEM file</param>
        /// <param name="password">Password</param>
        /// <param name="algorithm">Algorithm for PEM encryption</param>
        public static void SavePrivateKeyToPEM(RSACryptoServiceProvider rsa, string file, string password, string algorithm = "AES-256-CBC")
        {
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    PemWriter pemWriter = new PemWriter(sw);
                    AsymmetricCipherKeyPair ackp = DotNetUtilities.GetRsaKeyPair(rsa);
                    RsaPrivateCrtKeyParameters privKey = (RsaPrivateCrtKeyParameters)ackp.Private;
                    pemWriter.WriteObject(privKey, algorithm, password.ToCharArray(), new SecureRandom());
                }
            }
        }

        /// <summary>
        /// Save a private key to a PEM file
        /// </summary>
        /// <param name="rsa">Private key</param>
        /// <param name="file">PEM file</param>
        public static void SavePrivateKeyToPEM(RSACryptoServiceProvider rsa, string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    PemWriter pemWriter = new PemWriter(sw);
                    AsymmetricCipherKeyPair ackp = DotNetUtilities.GetRsaKeyPair(rsa);
                    RsaPrivateCrtKeyParameters privKey = (RsaPrivateCrtKeyParameters)ackp.Private;
                    pemWriter.WriteObject(privKey);
                }
            }
        }

        #endregion

        #region Save / Load Win KeyStore

        /// <summary>
        /// Save a key in the Windows KeyStore
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
        /// Load a key from the Windows KeyStore 
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
        /// Delete a key from the Windows KeyStore
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

        #region Save / Load XML

        /// <summary>
        /// Load Key from XML file
        /// </summary>
        /// <param name="file">XML file path</param>
        public static RSACryptoServiceProvider LoadKeyFromXml(string file)
        {
            string xmlString = File.ReadAllText(file, Encoding.Default);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlString);
            return rsa;
        }

        /// <summary>
        /// Save Key pair to XML file
        /// </summary>
        /// <param name="rsa">Key pair</param>
        /// <param name="file">XML file path</param>
        public static void SaveKeyPairToXml(RSACryptoServiceProvider rsa, string file)
        {
            string xmlString = rsa.ToXmlString(true);
            File.WriteAllText(file, xmlString, Encoding.Default);
        }

        /// <summary>
        /// Save public key to XML file
        /// </summary>
        /// <param name="rsa">Key</param>
        /// <param name="file">XML file path</param>
        public static void SavePublicKeyToXml(RSACryptoServiceProvider rsa, string file)
        {
            string xmlString = rsa.ToXmlString(false);
            File.WriteAllText(file, xmlString, Encoding.Default);
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
