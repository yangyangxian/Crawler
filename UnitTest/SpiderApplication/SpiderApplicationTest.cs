using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yang.Entities;
using Yang.SpiderApplication.Seashell;
using Yang.Utilities;

namespace UnitTest
{
    [TestClass]
    public class SpiderApplicationTest
    {
        [TestMethod]
        public async Task TestReadAllCommunitiesAsync()
        {
            SeashellApplications app = new SeashellApplications();

            List<Community> communities = await app.ReadAllCommunities();

            Assert.IsTrue(communities.Count > 0);
         
        }
    }
}