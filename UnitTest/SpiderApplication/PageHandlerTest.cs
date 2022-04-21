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

        [TestMethod]
        public async Task TestCommunityDetailAsync()
        {
            Community community = await SeashellPageHandlers.ReadCommunityDetailData("https://xa.ke.com/xiaoqu/3820028098488153/");

            Assert.IsTrue(community.BuildingNumber > 0 && community.Unit > 0 && !string.IsNullOrEmpty(community.HomeListURL));
        }

        [TestMethod]
        public async Task TestReadCommunityHomeListPageNumberAsync()
        {
            //This case is that the community has more than page of homes
            int pageNum = await CommunityHomeListPageHandler.ReadCommunityHomeListPageNumber("https://xa.ke.com/ershoufang/c3820028098488153/");
            Assert.IsTrue(pageNum > 0);

            //This case is that the community only one page of homes
            int pageNum1 = await CommunityHomeListPageHandler.ReadCommunityHomeListPageNumber("https://xa.ke.com/ershoufang/c3811057680286/");
            Assert.IsTrue(pageNum1 == 1);            
        }
    }
}