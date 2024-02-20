using AngleSharp.Dom;
using Newtonsoft.Json.Linq;
using SpiderApplication.Seashell.PageHandlers;
using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class HomeListPageHandler : PageHandler
    {

        public static async Task<CommunityHistoryInfo> ReadCommunityHistoryInfo(string url)
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            string communityNameText = document.QuerySelectorAll("div.agentCardResblockInfo a.agentCardResblockTitle")[0].InnerHtml;

            string listingPriceText = document.QuerySelectorAll("div.agentCardResblockInfo div.agentCardDetailInfo")[0].InnerHtml;
            string listingUnitText = document.QuerySelectorAll("div.agentCardResblockInfo div.agentCardDetailInfo")[1].InnerHtml;
            string transactionsInRecent90Days = document.QuerySelectorAll("div.agentCardResblockInfo div.agentCardDetailInfo")[2].InnerHtml;

            CommunityHistoryInfo historyInfo = new CommunityHistoryInfo();
            //historyInfo.CommunityName = communityNameText;
            historyInfo.CommunityListingPrice = decimal.TryParse(listingPriceText.Replace("元/平米", string.Empty), out decimal price) ? price : 0;
            historyInfo.CommunityListingUnits = int.TryParse(listingUnitText.Replace("套", string.Empty), out int unit) ? unit : 0;
            historyInfo.TransactionsInRecent90Days = int.TryParse(transactionsInRecent90Days.Replace("套", string.Empty), out int transactions) ? transactions : 0;
            historyInfo.DataTime = DateTime.Now.Date;

            return historyInfo;
        }

        public static async Task<int> ReadCommunityHomeListPageNumber(string url)
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            var cell = document.QuerySelector("div.house-lst-page-box");

            if (cell == null)
                return 1;

            var pageData = cell.GetAttribute("page-data");
            int totalPage = 0;

            try
            {
                JObject jsonObj = JObject.Parse(pageData);

                totalPage = Convert.ToInt32(jsonObj["totalPage"]);
            } 
            catch (Exception ex)
            {
                throw new Exception("page data is:" + pageData, ex);
            }

            return totalPage;
        }

        public static async Task<List<string>> ReadCommunityHomeURL(string url)
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            List<string> homeURLs = new List<string>();

            var homeItemList = document.QuerySelectorAll("ul.sellListContent li.clear");
            if (homeItemList == null)
                return homeURLs;

            foreach (var homeItem in homeItemList)
            {
                IElement homeTitle = homeItem.QuerySelector("div.info div.title a");

                if (homeTitle != null)
                    homeURLs.Add(homeTitle.GetAttribute("href"));
            }

            return homeURLs;
        }
    }
}
