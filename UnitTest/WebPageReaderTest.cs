using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Yang.Utilities;

namespace UnitTest
{
    [TestClass]
    public class WebPageReaderTest
    {
        [TestMethod]
        public async Task TestGetPageAsync()
        {
            string url = "https://xa.ke.com/xiaoqu/";

            var document = await WebPageReader.GetPageAsync(url);

            Assert.IsNotNull(document);
        }
    }
}