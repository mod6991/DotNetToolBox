using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using DotNetToolBox.Utils;
using System;
using System.Text;

namespace DotNetToolBox.Tester
{
    public static class SymCryptoTest
    {
        public static void Aes()
        {
            try
            {
                //Generate new random Key + IV
                byte[] key, iv;
                AESEncryptor.GenerateKeyIV(out key, out iv);

                string skey = HexadecimalEncoder.Encode(key);
                string siv = HexadecimalEncoder.Encode(iv);

                //Encrypt+Decrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = AESEncryptor.Encrypt(data, key, iv);
                byte[] dec = AESEncryptor.Decrypt(enc, key, iv);
                string sDec = Encoding.Default.GetString(dec);

                //Generate Key from password
                byte[] key2, salt, iv2;
                salt = RandomHelper.GenerateBytes(16);
                AESEncryptor.GenerateIV(out iv2);

                AESEncryptor.GenerateKeyFromPassword("test1234", salt, out key2);

                string skey2 = HexadecimalEncoder.Encode(key2);
                string ssalt = HexadecimalEncoder.Encode(salt);
                string siv2 = HexadecimalEncoder.Encode(iv2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Des()
        {
            try
            {
                //Generate new random Key + IV
                byte[] key, iv;
                DESEncryptor.GenerateKeyIV(out key, out iv);

                string skey = HexadecimalEncoder.Encode(key);
                string siv = HexadecimalEncoder.Encode(iv);

                //Encrypt+Decrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = DESEncryptor.Encrypt(data, key, iv);
                byte[] dec = DESEncryptor.Decrypt(enc, key, iv);
                string sDec = Encoding.Default.GetString(dec);

                //Generate Key from password
                byte[] key2, salt, iv2;
                salt = RandomHelper.GenerateBytes(16);
                DESEncryptor.GenerateIV(out iv2);

                DESEncryptor.GenerateKeyFromPassword("test1234", salt, out key2);

                string skey2 = HexadecimalEncoder.Encode(key2);
                string ssalt = HexadecimalEncoder.Encode(salt);
                string siv2 = HexadecimalEncoder.Encode(iv2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Rijndael()
        {
            try
            {
                //Generate new random Key + IV
                byte[] key, iv;
                RijndaelEncryptor.GenerateKeyIV(out key, out iv);

                string skey = HexadecimalEncoder.Encode(key);
                string siv = HexadecimalEncoder.Encode(iv);

                //Encrypt+Decrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = RijndaelEncryptor.Encrypt(data, key, iv);
                byte[] dec = RijndaelEncryptor.Decrypt(enc, key, iv);
                string sDec = Encoding.Default.GetString(dec);

                //Generate Key from password
                byte[] key2, salt, iv2;
                salt = RandomHelper.GenerateBytes(16);
                RijndaelEncryptor.GenerateIV(out iv2);

                RijndaelEncryptor.GenerateKeyFromPassword("test1234", salt, out key2);

                string skey2 = HexadecimalEncoder.Encode(key2);
                string ssalt = HexadecimalEncoder.Encode(salt);
                string siv2 = HexadecimalEncoder.Encode(iv2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void TripleDes()
        {
            try
            {
                //Generate new random Key + IV
                byte[] key, iv;
                TripleDESEncryptor.GenerateKeyIV(out key, out iv);

                string skey = HexadecimalEncoder.Encode(key);
                string siv = HexadecimalEncoder.Encode(iv);

                //Encrypt+Decrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = TripleDESEncryptor.Encrypt(data, key, iv);
                byte[] dec = TripleDESEncryptor.Decrypt(enc, key, iv);
                string sDec = Encoding.Default.GetString(dec);

                //Generate Key from password
                byte[] key2, salt, iv2;
                salt = RandomHelper.GenerateBytes(16);
                TripleDESEncryptor.GenerateIV(out iv2);

                TripleDESEncryptor.GenerateKeyFromPassword("test1234", salt, out key2);

                string skey2 = HexadecimalEncoder.Encode(key2);
                string ssalt = HexadecimalEncoder.Encode(salt);
                string siv2 = HexadecimalEncoder.Encode(iv2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Vernam()
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes("this is a secret message");
                byte[] key = RandomHelper.GenerateBytes(data.Length);
                byte[] enc = VernamEncryptor.EncryptDecrypt(data, key);
                string sEnc = Encoding.Default.GetString(enc);
                byte[] dec = VernamEncryptor.EncryptDecrypt(enc, key);
                string sdec = Encoding.Default.GetString(dec);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
