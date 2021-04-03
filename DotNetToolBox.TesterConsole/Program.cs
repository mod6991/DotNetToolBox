using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolBox.TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = Encoding.ASCII.GetBytes("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu");
            byte[] result;
            using (MemoryStream ms = new MemoryStream(data))
            {
                result = SHA3.Hash(ms);
            }

            string s = Hex.Encode(result);
        }
    }
}
