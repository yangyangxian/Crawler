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

            Community community2 = await SeashellPageHandlers.ReadCommunityDetailData("https://xa.ke.com/xiaoqu/3811057260042/");
            Assert.IsTrue(community2.BuildingNumber > 0 && community2.Unit > 0 && string.IsNullOrEmpty(community2.HomeListURL));
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

        [TestMethod]
        public async Task TestReadCommunityHomeURLAsync()
        {
            //This case is that the community has more than page of homes
            List<string> URLList = await CommunityHomeListPageHandler.ReadCommunityHomeURL("https://xa.ke.com/ershoufang/c3820028098488153/");
            Assert.IsTrue(URLList.Count > 0);
        }

        [TestMethod]
        public async Task TestReadCommunityHomeDetailAsync()
        {
            Home home = await CommunityHomePageHandler.ReadCommunityHomeDetail("https://xa.ke.com/ershoufang/101113524576.html?fb_expo_id=570767103242608644");
            Assert.IsTrue(home != null);
        }
    }
}