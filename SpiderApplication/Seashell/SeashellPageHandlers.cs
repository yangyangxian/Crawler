﻿using AngleSharp.Dom;
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
                        CommunityListingPrice = decimal.TryParse(listingPrice, out decimal price) ? price : 0,
                        CommunityListingUnits = int.TryParse(listingUnits, out int units) ? units : 0,
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

            string buildingText = document.QuerySelector("div.xiaoquInfo div:nth-child(5) span.xiaoquInfoContent").InnerHtml;
            string unitsText = document.QuerySelector("div.xiaoquInfo div:nth-child(6) span.xiaoquInfoContent").InnerHtml;

            int buildingNumber = int.Parse(buildingText.Remove(buildingText.IndexOf('栋')));
            int units = int.Parse(unitsText.Remove(unitsText.IndexOf('户')));

            Community community = new Community();
            community.BuildingNumber = buildingNumber;
            community.Unit = units;

            return community;
        }
    }
}