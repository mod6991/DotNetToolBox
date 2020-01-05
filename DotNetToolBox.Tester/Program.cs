using System;
using System.IO;
using System.Text;
using DotNetToolBox.IO;

namespace DotNetToolBox.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConfigurationTest.XmlConfig();
            //ConfigurationTest.IniConfig();

            //SymCryptoTest.Aes();
            //SymCryptoTest.Des();
            //SymCryptoTest.Rijndael();
            //SymCryptoTest.TripleDes();
            //SymCryptoTest.Vernam();
            //HashTest.Md5();
            //HashTest.Sha1();
            //HashTest.Sha256();
            //HashTest.Sha512();
            //RSATest.Test();
            //DNTBEncTest.Test();

            //DbManagerTest.Test();

            //IOTest.Test();
            //ReportTest.Test();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Col A;Col B;Col C;Col D");
            sb.AppendLine("a;;\"cc;c\";");

            string path = @"C:\Users\Jo\Downloads\FL_insurance_sample.csv";


            using (CsvReader csv = new CsvReader(path, Encoding.Default, true, ','))
            {
                csv.ReadColumns();

                while (csv.ReadLine())
                {
                    string policyID = csv["policyID"];
                    string line = csv["line"];
                }
            }

            Console.Read();
        }
    }
}
