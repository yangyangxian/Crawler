using AngleSharp.Dom;
using Newtonsoft.Json.Linq;
using SpiderApplication.Seashell.PageHandlers;
using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class HomeListPageHandler : PageHandler
    {

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
