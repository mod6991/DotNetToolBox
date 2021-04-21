using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNetToolBox.UnitTests
{
    [TestClass]
    public class SHA512Tests
    {
        private TestContext _testContext;
        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\sha512.csv", "sha512#csv", DataAccessMethod.Sequential)]
        public void Hash_Tests()
        {
            string sData = TestContext.DataRow["data"].ToString();
            string sHash = TestContext.DataRow["hash"].ToString();

            byte[] data = Hex.Decode(sData);
            byte[] hash = SHA512.Hash(data);

            string result = Hex.Encode(hash);

            Assert.AreEqual(sHash, result);
        }
    }
}
