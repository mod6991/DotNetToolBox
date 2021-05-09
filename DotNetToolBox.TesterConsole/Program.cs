using DotNetToolBox.TesterConsole.Tests;
using System;
using System.IO;
using System.Reflection;

namespace DotNetToolBox.TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StartTests();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal static void StartTests()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string path = Path.GetDirectoryName(assembly.Location);
                path = Path.Combine(path, "data");

                Test(Base64Tests.Encode, Path.Combine(path, "b64.dat"), "IO.Base64.Encode");
                Test(Base64Tests.Decode, Path.Combine(path, "b64.dat"), "IO.Base64.Decode");
                Test(HexTests.Encode, Path.Combine(path, "hex.dat"), "IO.Hex.Encode");
                Test(HexTests.Decode, Path.Combine(path, "hex.dat"), "IO.Hex.Decode");
                Console.WriteLine();

                Test(MD5Tests.Hash, Path.Combine(path, "md5.dat"), "Cryptography.MD5.Hash");
                Test(MD5Tests.HashStream, Path.Combine(path, "md5.dat"), "Cryptography.MD5.Hash stream");
                Test(SHA1Tests.Hash, Path.Combine(path, "sha1.dat"), "Cryptography.SHA1.Hash");
                Test(SHA1Tests.HashStream, Path.Combine(path, "sha1.dat"), "Cryptography.SHA1.Hash stream");
                Test(SHA256Tests.Hash, Path.Combine(path, "sha256.dat"), "Cryptography.SHA256.Hash");
                Test(SHA256Tests.HashStream, Path.Combine(path, "sha256.dat"), "Cryptography.SHA256.Hash stream");
                Test(SHA512Tests.Hash, Path.Combine(path, "sha512.dat"), "Cryptography.SHA512.Hash");
                Test(SHA512Tests.HashStream, Path.Combine(path, "sha512.dat"), "Cryptography.SHA512.Hash stream");
                Test(SHA3Tests.Hash, Path.Combine(path, "sha3.dat"), "Cryptography.SHA3.Hash");
                Test(SHA3Tests.HashStream, Path.Combine(path, "sha3.dat"), "Cryptography.SHA3.Hash stream");
                Console.WriteLine();

                Test(AESTests.EncryptCBC, Path.Combine(path, "aes.dat"), "Cryptography.AES.EncryptCBC");
                Test(AESTests.EncryptCBCStream, Path.Combine(path, "aes.dat"), "Cryptography.AES.EncryptCBC stream");
                Test(AESTests.DecryptCBC, Path.Combine(path, "aes.dat"), "Cryptography.AES.DecryptCBC");
                Test(AESTests.DecryptCBCStream, Path.Combine(path, "aes.dat"), "Cryptography.AES.DecryptCBC stream");
                Test(BlowfishTests.EncryptCBC, Path.Combine(path, "blowfish.dat"), "Cryptography.Blowfish.EncryptCBC");
                Test(BlowfishTests.EncryptCBCStream, Path.Combine(path, "blowfish.dat"), "Cryptography.Blowfish.EncryptCBC stream");
                Test(BlowfishTests.DecryptCBC, Path.Combine(path, "blowfish.dat"), "Cryptography.Blowfish.DecryptCBC");
                Test(BlowfishTests.DecryptCBCStream, Path.Combine(path, "blowfish.dat"), "Cryptography.Blowfish.DecryptCBC stream");
                Test(DESTests.EncryptCBC, Path.Combine(path, "des.dat"), "Cryptography.DES.EncryptCBC");
                Test(DESTests.EncryptCBCStream, Path.Combine(path, "des.dat"), "Cryptography.DES.EncryptCBC stream");
                Test(DESTests.DecryptCBC, Path.Combine(path, "des.dat"), "Cryptography.DES.DecryptCBC");
                Test(DESTests.DecryptCBCStream, Path.Combine(path, "des.dat"), "Cryptography.DES.DecryptCBC stream");
                Test(TripleDESTests.EncryptCBC, Path.Combine(path, "des3.dat"), "Cryptography.TripleDES.EncryptCBC");
                Test(TripleDESTests.EncryptCBCStream, Path.Combine(path, "des3.dat"), "Cryptography.TripleDES.EncryptCBC stream");
                Test(TripleDESTests.DecryptCBC, Path.Combine(path, "des3.dat"), "Cryptography.TripleDES.DecryptCBC");
                Test(TripleDESTests.DecryptCBCStream, Path.Combine(path, "des3.dat"), "Cryptography.TripleDES.DecryptCBC stream");
                Console.WriteLine();

                Test(ChaCha20Tests.Encrypt, Path.Combine(path, "chacha20.dat"), "Cryptography.ChaCha20.Encrypt");
                Test(ChaCha20Tests.EncryptStream, Path.Combine(path, "chacha20.dat"), "Cryptography.ChaCha20.Encrypt stream");
                Test(ChaCha20Tests.Decrypt, Path.Combine(path, "chacha20.dat"), "Cryptography.ChaCha20.Decrypt");
                Test(ChaCha20Tests.DecryptStream, Path.Combine(path, "chacha20.dat"), "Cryptography.ChaCha20.Decrypt stream");
                Test(ChaCha20Rfc7539Tests.Encrypt, Path.Combine(path, "chacha20rfc7539.dat"), "Cryptography.ChaCha20Rfc7539.Encrypt");
                Test(ChaCha20Rfc7539Tests.EncryptStream, Path.Combine(path, "chacha20rfc7539.dat"), "Cryptography.ChaCha20Rfc7539.Encrypt stream");
                Test(ChaCha20Rfc7539Tests.Decrypt, Path.Combine(path, "chacha20rfc7539.dat"), "Cryptography.ChaCha20Rfc7539.Decrypt");
                Test(ChaCha20Rfc7539Tests.DecryptStream, Path.Combine(path, "chacha20rfc7539.dat"), "Cryptography.ChaCha20Rfc7539.Decrypt stream");
                Test(Salsa20Tests.Encrypt, Path.Combine(path, "salsa20.dat"), "Cryptography.Salsa20.Encrypt");
                Test(Salsa20Tests.EncryptStream, Path.Combine(path, "salsa20.dat"), "Cryptography.Salsa20.Encrypt stream");
                Test(Salsa20Tests.Decrypt, Path.Combine(path, "salsa20.dat"), "Cryptography.Salsa20.Decrypt");
                Test(Salsa20Tests.DecryptStream, Path.Combine(path, "salsa20.dat"), "Cryptography.Salsa20.Decrypt stream");
                Console.WriteLine();

                Test(PBKDF2Tests.GenerateKey, Path.Combine(path, "pbkdf2.dat"), "Cryptography.PBKDF2.GenerateKeyFromPassword");
                Console.WriteLine();

                Test(PaddingIso7816Tests.Pad, Path.Combine(path, "padding_iso7816.dat"), "Cryptography.Padding.Pad Iso7816");
                Test(PaddingIso7816Tests.Unpad, Path.Combine(path, "padding_iso7816.dat"), "Cryptography.Padding.Unpad Iso7816");
                Test(PaddingPkcs7Tests.Pad, Path.Combine(path, "padding_pkcs7.dat"), "Cryptography.Padding.Pad Pkcs7");
                Test(PaddingPkcs7Tests.Unpad, Path.Combine(path, "padding_pkcs7.dat"), "Cryptography.Padding.Unpad Pkcs7");
                Test(PaddingX923Tests.Pad, Path.Combine(path, "padding_x923.dat"), "Cryptography.Padding.Pad AnsiX923");
                Test(PaddingX923Tests.Unpad, Path.Combine(path, "padding_x923.dat"), "Cryptography.Padding.Unpad AnsiX923");
                Console.WriteLine();

                Test(FileEncTests.EncryptDecrypt, null, "Cryptography.FileEnc");
                Console.WriteLine();

                Console.WriteLine("Press ENTER to exit...");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal static void Test(TestAction action, string file, string title)
        {
            Console.Write(title);

            try
            {
                int total = action(file);
                Console.WriteLine($" ({total} tests) --> OK");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed");
                Console.WriteLine("  " + ex.Message);
                Console.WriteLine();
            }
        }
    }

    internal delegate int TestAction(string file);
}
