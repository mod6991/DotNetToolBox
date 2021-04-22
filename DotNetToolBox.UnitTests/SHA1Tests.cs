﻿using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DotNetToolBox.UnitTests
{
    [TestClass]
    public class SHA1Tests
    {
        private TestContext _testContext;
        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\sha1.csv", "sha1#csv", DataAccessMethod.Sequential)]
        public void Hash_Tests()
        {
            string sData = TestContext.DataRow["data"].ToString();
            string sHash = TestContext.DataRow["hash"].ToString();

            byte[] data = Hex.Decode(sData);
            byte[] hash = SHA1.Hash(data);

            string result = Hex.Encode(hash);

            Assert.AreEqual(sHash, result);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\sha1.csv", "sha1#csv", DataAccessMethod.Sequential)]
        public void Hash_Stream_Tests()
        {
            string sData = TestContext.DataRow["data"].ToString();
            string sHash = TestContext.DataRow["hash"].ToString();

            byte[] data = Hex.Decode(sData);
            byte[] hash;

            using(MemoryStream ms = new MemoryStream(data))
            {
                hash = SHA1.Hash(ms);
            }

            string result = Hex.Encode(hash);

            Assert.AreEqual(sHash, result);
        }
    }
}
