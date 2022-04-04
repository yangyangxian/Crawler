using AngleSharp.Dom;
using Newtonsoft.Json.Linq;
using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class SeashellPageHandlers
    {
        //The url will be like 'https://xa.ke.com/xiaoqu/pg1/' which the number is the page number
        public static async Task<List<Community>> ReadCommunityListData(string url)
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            var communityItemList = document.QuerySelectorAll("ul.listContent li.xiaoquListItem");

            List<Community> communities = new List<Community>();
            foreach (var communityItem in communityItemList)
            {
                string communityName = communityItem.QuerySelector("div.info div.title a").InnerHtml;
                string districtName = communityItem.QuerySelector("div.info div.positionInfo a.district").InnerHtml;
                string neighborhood = communityItem.QuerySelector("div.info div.positionInfo a.bizcircle").InnerHtml;
                string listingPrice = communityItem.QuerySelector("div.xiaoquListItemRight div.xiaoquListItemPrice div.totalPrice span").InnerHtml;
                string listingUnits = communityItem.QuerySelector("div.xiaoquListItemRight div.xiaoquListItemSellCount a.totalSellCount span").InnerHtml;
                string seashellId = communityItem.GetAttribute("data-id");
                string seashellURL = communityItem.QuerySelector("div.info div.title a").GetAttribute("href");

                AdministrativeDistrict administrativeDistrict = AdministrativeDistrictRepository.GetByName(districtName);
                Community communityToAdd = new Community()
                {
                    CommunityName = communityName,
                    AdministrativeDistrictId = administrativeDistrict.AdministrativeDistrictId,
                    Neighborhood = neighborhood,
                    External_id = seashellId,
                    SeashellURL = seashellURL,
                    CommunityHistoryInfo = new List<CommunityHistoryInfo>().Append(new CommunityHistoryInfo()
                    {
                        CommunityListingPrice = decimal.Parse(listingPrice),
                        CommunityListingUnits = int.Parse(listingUnits),
                        DataTime = DateTime.Now
                    }).ToList()
                };

                communities.Add(communityToAdd);
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

        //The url will be like 'https://xa.ke.com/xiaoqu/3820028098488153/'
        public static async Task<Community> ReadCommunityDetailData(string url) 
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            var cellSelector = "div.title a.maidian-detail";
            var cells = document.QuerySelectorAll(cellSelector);

            List<string> priceList = new List<string>();
            List<string> building = new List<string>();
            List<string> unitList = new List<string>();

            foreach (var cell in cells)
            {
                //var url = cell.GetAttribute("href");
                //var documentChild = await BrowsingContext.New(config).OpenAsync(url);
                var priceSelector = "div.xiaoquDescribe span.xiaoquUnitPrice";
                var buildingNumSelector = "div.xiaoquDescribe div.xiaoquInfo div:nth-child(5) span.xiaoquInfoContent";
                var unitsSelector = "div.xiaoquDescribe div.xiaoquInfo div:nth-child(6) span.xiaoquInfoContent";

                //var unitsCell = documentChild.QuerySelectorAll(unitsSelector);

            }

            return new Community();
        }
    }
}
