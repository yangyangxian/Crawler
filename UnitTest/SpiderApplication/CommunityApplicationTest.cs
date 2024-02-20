using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
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
        public async Task TestGetHistoryInfoForCommunitiesInDatabse()
        {
            CommunityApplications app = new CommunityApplications();

            int updatedRecords = await app.GetHistoryInfoForCommunitiesInDatabse();

            Assert.IsTrue(updatedRecords > 0);
        }

        [TestMethod]
        public async Task TestRefreshAllCommunityInfoAsync()
        {
            CommunityApplications app = new CommunityApplications();

            int updatedRecords = await app.GetAndSaveCommunityInfo(false);

            Assert.IsTrue(updatedRecords > 0);
        }

        [TestMethod]
        public async Task TestRefreshCommunityInfoDatabase()
        {
            CommunityApplications app = new CommunityApplications();

            await app.RefreshCommunityInfoInDatabase();
        }
    }
}