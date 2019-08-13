using DotNetToolBox.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DotNetToolBox.Tester
{
    public static class RSATest
    {
        public static void Test()
        {
            try
            {
                //Generate new key pair
                RSACryptoServiceProvider rsa = RSAEncryptor.GenerateKeyPair(4096);

                //Export
                RSAEncryptor.SaveKeyPairToXml(rsa, @"C:\Temp\test_rsa.xml");
                RSAEncryptor.SavePublicKeyToPEM(rsa, @"C:\Temp\test_rsa_pub.pem");
                RSAEncryptor.SavePrivateKeyToPEM(rsa, @"C:\Temp\test_rsa_pk_nopass.pem");
                RSAEncryptor.SavePrivateKeyToPEM(rsa, @"C:\Temp\test_rsa_pk.pem", "test1234", "AES-256-CBC");
                RSAEncryptor.SaveInWinKeyStore(rsa, "TempTest01");

                //Encrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = RSAEncryptor.Encrypt(rsa, data);

                //Import + Decrypt
                RSACryptoServiceProvider pem = RSAEncryptor.LoadPrivateKeyFromPEM(@"C:\Temp\test_rsa_pk_nopass.pem");
                byte[] dec1 = RSAEncryptor.Decrypt(pem, enc);
                string sdec1 = Encoding.Default.GetString(dec1);

                RSACryptoServiceProvider pem2 = RSAEncryptor.LoadPrivateKeyFromPEM(@"C:\Temp\test_rsa_pk.pem", "test1234");
                byte[] dec2 = RSAEncryptor.Decrypt(pem2, enc);
                string sdec2 = Encoding.Default.GetString(dec2);

                RSACryptoServiceProvider xml = RSAEncryptor.LoadKeyFromXml(@"C:\Temp\test_rsa.xml");
                byte[] dec3 = RSAEncryptor.Decrypt(xml, enc);
                string sdec3 = Encoding.Default.GetString(dec3);

                RSACryptoServiceProvider winks = RSAEncryptor.LoadFromWinKeyStore("TempTest01");
                byte[] dec4 = RSAEncryptor.Decrypt(winks, enc);
                string sdec4 = Encoding.Default.GetString(dec4);

                RSAEncryptor.DeleteFromWinKeyStore("TempTest01");

                //Encrypt + decrypt password
                string encPassword = RSAEncryptor.EncryptPassword("test1234", rsa, Encoding.Default, RSAEncryptor.EncodingType.Base64);
                string decPassword = RSAEncryptor.DecryptPassword(encPassword, rsa, Encoding.Default, RSAEncryptor.EncodingType.Base64);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
