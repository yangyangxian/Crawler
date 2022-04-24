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
            string url = "https://ke-image.ljcdn.com/hdic-frame/standard_3bb359d7-db9d-46b5-af3b-982efc6bfa13.png!m_fill,w_1000,h_750,l_bk,f_jpg,ls_50?from=ke.com";

            var document = await WebPageReader.GetPageAsync(url);

            string stream = document.Source.Text;

            Assert.IsNotNull(stream);
        }
    }
}