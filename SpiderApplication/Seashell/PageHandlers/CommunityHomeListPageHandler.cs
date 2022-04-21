using AngleSharp.Dom;
using Newtonsoft.Json.Linq;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class CommunityHomeListPageHandler
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
    }
}
