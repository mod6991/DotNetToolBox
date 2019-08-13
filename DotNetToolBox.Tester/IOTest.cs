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
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
