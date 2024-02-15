using DataAPIs.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using Serilog.Core;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class CommunityControllerTest : TestBase
    {
        [TestMethod]
        public async Task TestRefreshCommunity()
        {
            CommunityController controller = new CommunityController();
            
            int count = await controller.RefreshCommunityData();

            Log.Logger.Information("Refresh compeleted: " + count + " records have been refreshed.");
        }
    }
}