using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    using System;
    using System.Collections.Generic;
    using Mirror;

    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_ParseQuery()
        {
            Uri uri = new Uri("https://example.com?q=foo&token=asdf");
            string query = uri.Query;
            Dictionary<string, string> parsed = Helper.ParseQuery(query);
            Assert.AreEqual(2, parsed.Keys.Count);
        }

        [TestMethod]
        public void Test_ParseQuery_Empty()
        {
            Uri uri = new Uri("https://example.com");
            string query = uri.Query;
            Dictionary<string, string> parsed = Helper.ParseQuery(query);
            Assert.AreEqual(0, parsed.Keys.Count);
        }

        [TestMethod]
        public void Test_EncodeQuery()
        {
            Uri uri = new Uri("https://example.com?q=foo&token=asdf");
            string query = uri.Query;
            string encoded = Helper.EncodeQuery(query);
            // Console.WriteLine(encoded);
            Assert.AreEqual("cTtmb290b2tlbjthc2Rm", encoded);
        }

        [TestMethod]
        public void Test_JsonMirror()
        {
            string domain = "https://api.limeade.cloud";
            string path = "/api/diagnostic/healthcheck?q=true";
            var client = new MirrorClient(domain);
            var resp = client.GetAsync(path);
            var storage = new BlobStorage(domain);
            var cached = storage.Get(path);
            Assert.IsNotNull(cached);
        }
    }
}
