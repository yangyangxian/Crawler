using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Yang.Entities;

namespace UnitTest
{
    [TestClass]
    public class ContextTest
    {
        [TestMethod]
        public async Task TestDatabaseConnection()
        {
            SeashellContext context = new SeashellContext();

            Community community = context.Communities.FirstOrDefault();

            Assert.IsNotNull(community);
        }
    }
}
