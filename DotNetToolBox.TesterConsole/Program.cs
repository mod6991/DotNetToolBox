using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using DotNetToolBox.Utils;
using Org.BouncyCastle.Crypto.Paddings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNetToolBox.TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                byte[] data = new byte[24];
                byte[] padded = PaddingPkcs7.Pad(data, 16);
                byte[] unpadded = PaddingPkcs7.UnPad(padded, 16);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static bool CheckArrays(byte[] data1, byte[] data2)
        {
            for(int i = 0; i < data1.Length; i++)
            {
                if (data1[i] != data2[i])
                    return false;
            }

            return true;
        }
    }
}