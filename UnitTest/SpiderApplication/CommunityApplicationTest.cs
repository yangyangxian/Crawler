using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yang.Entities;
using Yang.SpiderApplication.Seashell;
using Yang.Utilities;

namespace UnitTest
{
    [TestClass]
    public class CommunityApplicationTest : TestBase
    {
        [TestMethod]
        public async Task TestReadAllCommunitiesAsync()
        {
            CommunityApplications app = new CommunityApplications();

            List<Community> communities = await app.ReadCommunities();

            Assert.IsTrue(communities.Count > 0);         
        }

        [TestMethod]
        public async Task TestGetAndRefreshAllCommunityInfoAsync()
        {
            CommunityApplications app = new CommunityApplications();

            int updatedRecords = await app.GetAndRefreshAllCommunityInfo();

            Assert.IsTrue(updatedRecords > 0);
        }

        [TestMethod]
        public async Task TestRefreshCommunityBasicInfoAsync()
        {
            CommunityApplications app = new CommunityApplications();

            int updatedRecords = await app.RefreshExistingCommunityBasicInfo();

            Assert.IsTrue(updatedRecords > 0);
        }
    }
}