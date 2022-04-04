using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yang.Entities;
using Yang.SpiderApplication.Seashell;
using Yang.Utilities;

namespace UnitTest
{
    [TestClass]
    public class PageHandlerTest
    {
        [TestMethod]
        public async Task TestReadCommunityListPageLinksAsync()
        {
            int pageNum = await SeashellPageHandlers.ReadCommunityListPageNumber(string.Format(SeashellConst.CommunityMainPageXianURL, 1));

            Assert.IsTrue(pageNum > 0);
        }

        [TestMethod]
        public async Task TestReadCommunityListDataAsync()
        {
            List<Community> communities = await SeashellPageHandlers.ReadCommunityListData(string.Format(SeashellConst.CommunityMainPageXianURL, 1));

            Assert.IsTrue(communities.Count > 0);
        }
    }
}