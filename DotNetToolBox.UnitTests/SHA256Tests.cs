using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNetToolBox.UnitTests
{
    [TestClass]
    public class SHA256Tests
    {
        private TestContext _testContext;
        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\sha256.csv", "sha256#csv", DataAccessMethod.Sequential)]
        public void Hash_Tests()
        {
            string sData = TestContext.DataRow["data"].ToString();
            string sHash = TestContext.DataRow["hash"].ToString();

            byte[] data = Hex.Decode(sData);
            byte[] hash = SHA256.Hash(data);

            string result = Hex.Encode(hash);

            Assert.AreEqual(sHash, result);
        }
    }
}
