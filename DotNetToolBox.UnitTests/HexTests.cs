using DotNetToolBox.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DotNetToolBox.UnitTests
{
    [TestClass]
    public class HexTests
    {
        private TestContext _testContext;
        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\hex.csv", "hex#csv", DataAccessMethod.Sequential)]
        public void Encode_Tests()
        {
            string sHex = TestContext.DataRow["enc_str"].ToString();
            string sBytesCsv = TestContext.DataRow["bytes_csv"].ToString();

            byte[] data = ParseBytesCsv(sBytesCsv, '-');
            string encoded = Hex.Encode(data);

            Assert.AreEqual(sHex, encoded);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\hex.csv", "hex#csv", DataAccessMethod.Sequential)]
        public void Decode_Tests()
        {
            string sHex = TestContext.DataRow["enc_str"].ToString();
            string sBytesCsv = TestContext.DataRow["bytes_csv"].ToString();

            byte[] data = ParseBytesCsv(sBytesCsv, '-');
            byte[] decoded = Hex.Decode(sHex);

            bool result = CheckArrays(data, decoded);
            Assert.AreEqual(true, result);
        }

        byte[] ParseBytesCsv(string line, char delimiter)
        {
            string[] sValues = line.Split(delimiter);
            List<byte> values = new List<byte>();
            foreach (string sb in sValues)
            {
                if (!string.IsNullOrWhiteSpace(sb))
                    values.Add(byte.Parse(sb));
            }
            return values.ToArray();
        }

        bool CheckArrays(byte[] data1, byte[] data2)
        {
            for (int i = 0; i < data1.Length; i++)
            {
                if (data1[i] != data2[i])
                    return false;
            }

            return true;
        }
    }
}
