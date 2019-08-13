using DotNetToolBox.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetToolBox.Tester
{
    public static class IOTest
    {
        public static void Test()
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes("0123456789ABCDEF");

                string sBase64 = Base64Encoder.Encode(data);
                string sHexadecimal = HexadecimalEncoder.Encode(data);
                byte[] gzip = GZipCompressor.Compress(data);

                StreamHelper.WriteBytes(data, @"C:\Temp\file.bin");
                byte[] fileContent = StreamHelper.ReadBytes(@"C:\Temp\file.bin");

                StreamHelper.WriteString("sample text", @"C:\Temp\file.txt", Encoding.UTF8);
                string sFileContent = StreamHelper.ReadString(@"C:\Temp\file.txt", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
