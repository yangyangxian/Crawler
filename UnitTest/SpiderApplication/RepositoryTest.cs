using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yang.Entities;
using Yang.SpiderApplication.Seashell;

namespace UnitTest
{
    [TestClass]
    public class RepositoryTest : TestBase
    {
        [TestMethod]
        public async Task TestAddOrUpdate()
        {
            //SeashellContext context = new SeashellContext();
            //CommunityHistoryInfoRepository repo = new CommunityHistoryInfoRepository(context);

            //CommunityHistoryInfo communityHistoryInfo = new CommunityHistoryInfo() { CommunityId = 2, DataTime = DateTime.Now, CommunityListingPrice = 10, CommunityListingUnits = 100 };

            //CommunityHistoryInfo existingEntity = context.CommunityHistoryInfos.Where(c => c.CommunityId == communityHistoryInfo.CommunityId).FirstOrDefault();

            //if (existingEntity == null)
            //{
            //    repo.AddOrUpdate(existingEntity);
            //} 


            //repo.AddOrUpdate();

            //Assert.IsTrue(communities.Count > 0);         
        }
    }
}