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
                AES.GenerateKeyIV(out key, out iv);

                string skey = Hex.Encode(key);
                string siv = Hex.Encode(iv);

                //Encrypt+Decrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = AES.Encrypt(data, key, iv);
                byte[] dec = AES.Decrypt(enc, key, iv);
                string sDec = Encoding.Default.GetString(dec);

                //Generate Key from password
                byte[] key2, salt, iv2;
                salt = RandomHelper.GenerateBytes(16);
                AES.GenerateIV(out iv2);

                AES.GenerateKeyFromPassword("test1234", salt, out key2);

                string skey2 = Hex.Encode(key2);
                string ssalt = Hex.Encode(salt);
                string siv2 = Hex.Encode(iv2);
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
                DES.GenerateKeyIV(out key, out iv);

                string skey = Hex.Encode(key);
                string siv = Hex.Encode(iv);

                //Encrypt+Decrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = DES.Encrypt(data, key, iv);
                byte[] dec = DES.Decrypt(enc, key, iv);
                string sDec = Encoding.Default.GetString(dec);

                //Generate Key from password
                byte[] key2, salt, iv2;
                salt = RandomHelper.GenerateBytes(16);
                DES.GenerateIV(out iv2);

                DES.GenerateKeyFromPassword("test1234", salt, out key2);

                string skey2 = Hex.Encode(key2);
                string ssalt = Hex.Encode(salt);
                string siv2 = Hex.Encode(iv2);
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
                Cryptography.Rijndael.GenerateKeyIV(out key, out iv);

                string skey = Hex.Encode(key);
                string siv = Hex.Encode(iv);

                //Encrypt+Decrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = Cryptography.Rijndael.Encrypt(data, key, iv);
                byte[] dec = Cryptography.Rijndael.Decrypt(enc, key, iv);
                string sDec = Encoding.Default.GetString(dec);

                //Generate Key from password
                byte[] key2, salt, iv2;
                salt = RandomHelper.GenerateBytes(16);
                Cryptography.Rijndael.GenerateIV(out iv2);

                Cryptography.Rijndael.GenerateKeyFromPassword("test1234", salt, out key2);

                string skey2 = Hex.Encode(key2);
                string ssalt = Hex.Encode(salt);
                string siv2 = Hex.Encode(iv2);
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
                TripleDES.GenerateKeyIV(out key, out iv);

                string skey = Hex.Encode(key);
                string siv = Hex.Encode(iv);

                //Encrypt+Decrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = TripleDES.Encrypt(data, key, iv);
                byte[] dec = TripleDES.Decrypt(enc, key, iv);
                string sDec = Encoding.Default.GetString(dec);

                //Generate Key from password
                byte[] key2, salt, iv2;
                salt = RandomHelper.GenerateBytes(16);
                TripleDES.GenerateIV(out iv2);

                TripleDES.GenerateKeyFromPassword("test1234", salt, out key2);

                string skey2 = Hex.Encode(key2);
                string ssalt = Hex.Encode(salt);
                string siv2 = Hex.Encode(iv2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
