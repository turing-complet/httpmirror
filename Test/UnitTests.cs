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
    }
}
