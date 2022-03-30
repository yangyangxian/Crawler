using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
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
            string url = "https://www.douban.com";
            Exception exception = null;

            try
            {
                var document = await WebPageReader.GetPageAsync(url);

                Assert.IsNotNull(document);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNull(exception);
        }
    }
}