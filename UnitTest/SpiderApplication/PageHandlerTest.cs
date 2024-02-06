using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpiderApplication.Seashell.PageHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yang.Entities;
using Yang.SpiderApplication.Seashell;
using Yang.Utilities;

namespace UnitTest
{
    [TestClass]
    public class PageHandlerTest : TestBase
    {
        [TestMethod]
        public async Task TestReadCommunityListPageLinksAsync()
        {
            int pageNum = await CommunityListPageHandler.ReadCommunityListPageNumber(string.Format(SeashellConst.CommunityMainPageBeilinURL, 1));

            Assert.IsTrue(pageNum > 0);
        }

        [TestMethod]
        public async Task TestReadCommunityListDataAsync()
        {
            List<Community> communities = await CommunityListPageHandler.ReadCommunityListData(string.Format(SeashellConst.CommunityMainPageBeilinURL, 6));

            Assert.IsTrue(communities.Count > 0);
        }

        [TestMethod]
        public async Task TestCommunityDetailAsync()
        {
            Community community = await CommunityPageHandler.ReadCommunityDetailData("https://xa.ke.com/xiaoqu/3820028098488153/");
            Assert.IsTrue(community.BuildingNumber > 0 && community.Unit > 0 && !string.IsNullOrEmpty(community.HomeListURL));

            Community community2 = await CommunityPageHandler.ReadCommunityDetailData("https://xa.ke.com/xiaoqu/3811057260042/");
            Assert.IsTrue(community2.BuildingNumber > 0 && community2.Unit > 0 && string.IsNullOrEmpty(community2.HomeListURL));
        }

        [TestMethod]
        public async Task TestReadCommunityHomeListPageNumberAsync()
        {
            //This case is that the community has more than page of homes
            int pageNum = await HomeListPageHandler.ReadCommunityHomeListPageNumber("https://xa.ke.com/ershoufang/c3820028098488153/");
            Assert.IsTrue(pageNum > 0);

            //This case is that the community only one page of homes
            int pageNum1 = await HomeListPageHandler.ReadCommunityHomeListPageNumber("https://xa.ke.com/ershoufang/c3811057680286/");
            Assert.IsTrue(pageNum1 == 1);            
        }

        [TestMethod]
        public async Task TestReadCommunityHomeURLAsync()
        {
            //This case is that the community has more than page of homes
            List<string> URLList = await HomeListPageHandler.ReadCommunityHomeURL("https://xa.ke.com/ershoufang/c3820028098488153/");
            Assert.IsTrue(URLList.Count > 0);
        }

        [TestMethod]
        public async Task TestReadCommunityHomeDetailAsync()
        {
            Home home = await HomePageHandler.ReadCommunityHomeDetail("https://xa.ke.com/ershoufang/101111499576.html?fb_expo_id=570774267071905799");
            Assert.IsTrue(home != null);
        }
    }
}