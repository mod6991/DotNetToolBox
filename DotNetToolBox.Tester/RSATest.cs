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
                RSACryptoServiceProvider rsa = Cryptography.RSA.GenerateKeyPair(4096);

                //Export
                Cryptography.RSA.SaveKeyPairToXml(rsa, @"C:\Temp\test_rsa.xml");
                Cryptography.RSA.SavePublicKeyToPEM(rsa, @"C:\Temp\test_rsa_pub.pem");
                Cryptography.RSA.SavePrivateKeyToPEM(rsa, @"C:\Temp\test_rsa_pk_nopass.pem");
                Cryptography.RSA.SavePrivateKeyToPEM(rsa, @"C:\Temp\test_rsa_pk.pem", "test1234", "AES-256-CBC");
                Cryptography.RSA.SaveInWinKeyStore(rsa, "TempTest01");

                //Encrypt
                byte[] data = Encoding.Default.GetBytes("This is a secret message");
                byte[] enc = Cryptography.RSA.Encrypt(rsa, data);

                //Import + Decrypt
                RSACryptoServiceProvider pem = Cryptography.RSA.LoadPrivateKeyFromPEM(@"C:\Temp\test_rsa_pk_nopass.pem");
                byte[] dec1 = Cryptography.RSA.Decrypt(pem, enc);
                string sdec1 = Encoding.Default.GetString(dec1);

                RSACryptoServiceProvider pem2 = Cryptography.RSA.LoadPrivateKeyFromPEM(@"C:\Temp\test_rsa_pk.pem", "test1234");
                byte[] dec2 = Cryptography.RSA.Decrypt(pem2, enc);
                string sdec2 = Encoding.Default.GetString(dec2);

                RSACryptoServiceProvider xml = Cryptography.RSA.LoadKeyFromXml(@"C:\Temp\test_rsa.xml");
                byte[] dec3 = Cryptography.RSA.Decrypt(xml, enc);
                string sdec3 = Encoding.Default.GetString(dec3);

                RSACryptoServiceProvider winks = Cryptography.RSA.LoadFromWinKeyStore("TempTest01");
                byte[] dec4 = Cryptography.RSA.Decrypt(winks, enc);
                string sdec4 = Encoding.Default.GetString(dec4);

                Cryptography.RSA.DeleteFromWinKeyStore("TempTest01");

                //Encrypt + decrypt password
                string encPassword = Cryptography.RSA.EncryptPassword("test1234", rsa, Encoding.Default, Cryptography.RSA.EncodingType.Base64);
                string decPassword = Cryptography.RSA.DecryptPassword(encPassword, rsa, Encoding.Default, Cryptography.RSA.EncodingType.Base64);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
