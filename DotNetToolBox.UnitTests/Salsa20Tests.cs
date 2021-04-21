using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNetToolBox.UnitTests
{
    [TestClass]
    public class Salsa20Tests
    {
        private TestContext _testContext;
        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\salsa20.csv", "salsa20#csv", DataAccessMethod.Sequential)]
        public void Encrypt_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sNonce = TestContext.DataRow["nonce"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] nonce = Hex.Decode(sNonce);
            byte[] data = Hex.Decode(sData);

            byte[] enc = Salsa20.Encrypt(data, key, nonce);
            string result = Hex.Encode(enc);

            Assert.AreEqual(sEnc, result);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\salsa20.csv", "salsa20#csv", DataAccessMethod.Sequential)]
        public void Decrypt_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sNonce = TestContext.DataRow["nonce"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] nonce = Hex.Decode(sNonce);
            byte[] enc = Hex.Decode(sEnc);

            byte[] dec = Salsa20.Decrypt(enc, key, nonce);
            string result = Hex.Encode(dec);

            Assert.AreEqual(sData, result);
        }
    }
}
