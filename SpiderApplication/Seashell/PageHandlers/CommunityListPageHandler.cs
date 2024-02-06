using AngleSharp.Dom;
using Newtonsoft.Json.Linq;
using Serilog;
using Yang.Entities;
using Yang.Utilities;

namespace SpiderApplication.Seashell.PageHandlers
{
    public class CommunityListPageHandler : PageHandler
    {
        //The url will be like 'https://xa.ke.com/xiaoqu/pg1/' which the number is the page number
        public static async Task<List<Community>> ReadCommunityListData(string url)
        {
            List<Community> communities = new List<Community>();
            try
            {
                IDocument document = await WebPageReader.GetPageAsync(url);
                 
                var communityItemList = document.QuerySelectorAll("ul.listContent li.xiaoquListItem");

                Log.Logger.Information("The count returned from " + url + " is " + communityItemList.Count());

                AdministrativeDistrictRepository administrativeDistrictRepository = new AdministrativeDistrictRepository(context);

                foreach (var communityItem in communityItemList)
                {
                    string communityName = communityItem.QuerySelector("div.info div.title a").InnerHtml;
                    string districtName = communityItem.QuerySelector("div.info div.positionInfo a.district").InnerHtml;
                    string neighborhood = communityItem.QuerySelector("div.info div.positionInfo a.bizcircle").InnerHtml;
                    string listingPrice = communityItem.QuerySelector("div.xiaoquListItemRight div.xiaoquListItemPrice div.totalPrice span").InnerHtml;
                    string listingUnits = communityItem.QuerySelector("div.xiaoquListItemRight div.xiaoquListItemSellCount a.totalSellCount span").InnerHtml;
                    string seashellId = communityItem.GetAttribute("data-id");
                    string seashellURL = communityItem.QuerySelector("div.info div.title a").GetAttribute("href");

                    AdministrativeDistrict administrativeDistrict = administrativeDistrictRepository.GetByName(districtName);
                    Community communityToAdd = new Community()
                    {
                        CommunityName = communityName,
                        AdministrativeDistrictId = administrativeDistrict.AdministrativeDistrictId,
                        Neighborhood = neighborhood,
                        External_id = seashellId,
                        SeashellURL = seashellURL,
                        CommunityHistoryInfo = new List<CommunityHistoryInfo>().Append(new CommunityHistoryInfo()
                        {
                            CommunityListingPrice = decimal.TryParse(listingPrice, out decimal price) ? price : 0,
                            CommunityListingUnits = int.TryParse(listingUnits, out int units) ? units : 0,
                            CommunityName = communityName,
                            DataTime = DateTime.Now.Date
                        }).ToList()
                    };

                    communities.Add(communityToAdd);
                }
            } catch (Exception ex)
            {
                Log.Logger.Error(ex, "The url is " + url);
            }

            return communities;
        }

        //The url will be like 'https://xa.ke.com/xiaoqu/'
        public static async Task<int> ReadCommunityListPageNumber(string url)
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            var cell = document.QuerySelector("div.house-lst-page-box");

            var pageData = cell.GetAttribute("page-data");

            JObject jsonObj = JObject.Parse(pageData);

            int totalPage = Convert.ToInt32(jsonObj["totalPage"]);

            return totalPage;
        }       
    }
}
