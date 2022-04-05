using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yang.Entities;

namespace UnitTest
{
    [TestClass]
    public class Playground
    {
        [TestMethod]
        public async Task TestListAsync()
        {
            List<Community> com = new List<Community>() { new Community() { CommunityName = "zhonghai"} };

            List<Community> sub = com.Take(1).ToList();

            for (int i = 0; i < 100; i = i + 1)
            {
                i = i + 2132;
            }

            sub[0].CommunityName = "rongchuang";

            Assert.IsNotNull(com);
        }
    }
}