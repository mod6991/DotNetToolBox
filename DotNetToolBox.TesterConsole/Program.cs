using DotNetToolBox.TesterConsole.Tests;
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
