using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DotNetToolBox.UnitTests
{
    [TestClass]
    public class TripleDESTests
    {
        private TestContext _testContext;
        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\des3.csv", "des3#csv", DataAccessMethod.Sequential)]
        public void Encrypt_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sIv = TestContext.DataRow["iv"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] iv = Hex.Decode(sIv);
            byte[] data = Hex.Decode(sData);

            byte[] enc = TripleDES.Encrypt(data, key, iv);
            string result = Hex.Encode(enc);

            Assert.AreEqual(sEnc, result);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\des3.csv", "des3#csv", DataAccessMethod.Sequential)]
        public void Encrypt_Stream_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sIv = TestContext.DataRow["iv"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] iv = Hex.Decode(sIv);
            byte[] data = Hex.Decode(sData);

            byte[] enc;
            using(MemoryStream msIn = new MemoryStream(data))
            {
                using (MemoryStream msOut = new MemoryStream())
                {
                    TripleDES.Encrypt(msIn, msOut, key, iv, PaddingStyle.None);
                    enc = msOut.ToArray();
                }
            }

            string result = Hex.Encode(enc);

            Assert.AreEqual(sEnc, result);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\des3.csv", "des3#csv", DataAccessMethod.Sequential)]
        public void Decrypt_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sIv = TestContext.DataRow["iv"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] iv = Hex.Decode(sIv);
            byte[] enc = Hex.Decode(sEnc);

            byte[] dec = TripleDES.Decrypt(enc, key, iv);
            string result = Hex.Encode(dec);

            Assert.AreEqual(sData, result);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\des3.csv", "des3#csv", DataAccessMethod.Sequential)]
        public void Decrypt_Stream_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sIv = TestContext.DataRow["iv"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] iv = Hex.Decode(sIv);
            byte[] enc = Hex.Decode(sEnc);

            byte[] dec;
            using(MemoryStream msIn = new MemoryStream(enc))
            {
                using (MemoryStream msOut = new MemoryStream())
                {
                    TripleDES.Decrypt(msIn, msOut, key, iv, PaddingStyle.None);
                    dec = msOut.ToArray();
                }
            }

            string result = Hex.Encode(dec);

            Assert.AreEqual(sData, result);
        }
    }
}
