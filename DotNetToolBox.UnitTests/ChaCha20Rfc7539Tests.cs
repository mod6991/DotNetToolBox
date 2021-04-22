﻿using DotNetToolBox.Cryptography;
using DotNetToolBox.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace DotNetToolBox.UnitTests
{
    [TestClass]
    public class ChaCha20Rfc7539Tests
    {
        private TestContext _testContext;
        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\chacha20rfc7539.csv", "chacha20rfc7539#csv", DataAccessMethod.Sequential)]
        public void Encrypt_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sNonce = TestContext.DataRow["nonce"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] nonce = Hex.Decode(sNonce);
            byte[] data = Hex.Decode(sData);

            byte[] enc = ChaCha20Rfc7539.Encrypt(data, key, nonce);
            string result = Hex.Encode(enc);

            Assert.AreEqual(sEnc, result);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\chacha20rfc7539.csv", "chacha20rfc7539#csv", DataAccessMethod.Sequential)]
        public void Encrypt_Stream_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sNonce = TestContext.DataRow["nonce"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] nonce = Hex.Decode(sNonce);
            byte[] data = Hex.Decode(sData);

            byte[] enc;
            using(MemoryStream msIn = new MemoryStream(data))
            {
                using (MemoryStream msOut = new MemoryStream())
                {
                    ChaCha20Rfc7539.Encrypt(msIn, msOut, key, nonce);
                    enc = msOut.ToArray();
                }
            }

            string result = Hex.Encode(enc);

            Assert.AreEqual(sEnc, result);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\chacha20rfc7539.csv", "chacha20rfc7539#csv", DataAccessMethod.Sequential)]
        public void Decrypt_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sNonce = TestContext.DataRow["nonce"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] nonce = Hex.Decode(sNonce);
            byte[] enc = Hex.Decode(sEnc);

            byte[] dec = ChaCha20Rfc7539.Decrypt(enc, key, nonce);
            string result = Hex.Encode(dec);

            Assert.AreEqual(sData, result);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\data\chacha20rfc7539.csv", "chacha20rfc7539#csv", DataAccessMethod.Sequential)]
        public void Decrypt_Stream_Tests()
        {
            string sKey = TestContext.DataRow["key"].ToString();
            string sNonce = TestContext.DataRow["nonce"].ToString();
            string sData = TestContext.DataRow["data"].ToString();
            string sEnc = TestContext.DataRow["enc"].ToString();

            byte[] key = Hex.Decode(sKey);
            byte[] nonce = Hex.Decode(sNonce);
            byte[] enc = Hex.Decode(sEnc);

            byte[] dec;
            using(MemoryStream msIn = new MemoryStream(enc))
            {
                using (MemoryStream msOut = new MemoryStream())
                {
                    ChaCha20Rfc7539.Decrypt(msIn, msOut, key, nonce);
                    dec = msOut.ToArray();
                }
            }

            string result = Hex.Encode(dec);

            Assert.AreEqual(sData, result);
        }
    }
}