﻿using DotNetToolBox.TesterConsole.Tests;
using System;
using System.IO;

namespace DotNetToolBox.TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = @"C:\Temp\testData";

                Test(Base64Tests.Encode, Path.Combine(path, "b64.dat"), "IO.Base64.Encode: ");
                Test(Base64Tests.Decode, Path.Combine(path, "b64.dat"), "IO.Base64.Decode: ");
                Test(HexTests.Encode, Path.Combine(path, "hex.dat"), "IO.Hex.Encode: ");
                Test(HexTests.Decode, Path.Combine(path, "hex.dat"), "IO.Hex.Decode: ");
                Test(MD5Tests.Hash, Path.Combine(path, "md5.dat"), "Cryptography.MD5.Hash: ");
                Test(MD5Tests.HashStream, Path.Combine(path, "md5.dat"), "Cryptography.MD5.Hash 2: ");
                Test(SHA1Tests.Hash, Path.Combine(path, "sha1.dat"), "Cryptography.SHA1.Hash: ");
                Test(SHA1Tests.HashStream, Path.Combine(path, "sha1.dat"), "Cryptography.SHA1.Hash 2: ");
                Test(SHA256Tests.Hash, Path.Combine(path, "sha256.dat"), "Cryptography.SHA256.Hash: ");
                Test(SHA256Tests.HashStream, Path.Combine(path, "sha256.dat"), "Cryptography.SHA256.Hash 2: ");
                Test(SHA512Tests.Hash, Path.Combine(path, "sha512.dat"), "Cryptography.SHA512.Hash: ");
                Test(SHA512Tests.HashStream, Path.Combine(path, "sha512.dat"), "Cryptography.SHA512.Hash 2: ");
                Test(SHA3Tests.Hash, Path.Combine(path, "sha3.dat"), "Cryptography.SHA3.Hash: ");
                Test(SHA3Tests.HashStream, Path.Combine(path, "sha3.dat"), "Cryptography.SHA3.Hash 2: ");

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static void Test(Action<string> action, string file, string title)
        {
            Console.Write(title);

            try
            {
                action(file);
                Console.WriteLine("OK");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed");
                Console.WriteLine("  " + ex.Message);
                Console.WriteLine();
            }
        }
    }
}
