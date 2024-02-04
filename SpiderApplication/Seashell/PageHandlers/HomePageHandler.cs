using AngleSharp.Dom;
using Newtonsoft.Json.Linq;
using SpiderApplication.Seashell.PageHandlers;
using System.Text.RegularExpressions;
using Yang.Entities;
using Yang.Utilities;

namespace Yang.SpiderApplication.Seashell
{
    public class HomePageHandler : PageHandler
    {

        public static async Task<Home> ReadCommunityHomeDetail(string url)
        {
            IDocument document = await WebPageReader.GetPageAsync(url);

            IHtmlCollection<IElement> infoListItem = document.QuerySelectorAll("div.base div.content ul li");
            foreach (IElement item in infoListItem)
            {
                item.RemoveChild(item.FirstChild);
            }

            int bedrooms = 0;
            int bathrooms = 0;
            int totalFloors = 0;
            decimal constructionArea = 0;

            Regex roomInfoReg = new Regex("[0-9]室[0-9]厅[0-9]卫");
            string roomInfo = infoListItem[0].InnerHtml;
            if (roomInfoReg.IsMatch(roomInfo))
            {
                bedrooms = int.Parse(roomInfo.Substring(0, 1));
                bathrooms = int.Parse(roomInfo.Substring(4, 1));
            } 
            else throw new Exception("The format of roomInfo is not as expected:" + roomInfo);

            Regex floorInfoReg = new Regex("[高中低]楼层 [(]共[0-9]{1,2}层[)]");
            string floorInfo = infoListItem[1].InnerHtml;
            if (floorInfoReg.IsMatch(floorInfo))
            {
                totalFloors = int.Parse(System.Text.RegularExpressions.Regex.Replace(floorInfo, @"[^0-9]+", ""));
            }
            else throw new Exception("The format of floorInfo is not as expected:" + floorInfo); 

            Regex areaReg = new Regex("^([1-9][0-9]*)+(.{0,1}[0-9]{0,2})㎡");
            string area = infoListItem[2].InnerHtml;
            if (areaReg.IsMatch(area))
            {
                decimal.TryParse(area.Substring(0, area.Length - 1), out constructionArea);                
            } 
            else throw new Exception("The format of area is not as expected:" + area);

            decimal totalPrice = 0;
            IElement priceEle = document.QuerySelector("div.price span.total");
            if (priceEle != null && !decimal.TryParse(priceEle.InnerHtml, out totalPrice))
                throw new Exception("The format of priceEle is not as expected:" + priceEle.InnerHtml);

            decimal totalFloorArea = 0;
            string floorAreaDetail = string.Empty;
            IHtmlCollection<IElement> roomList = document.QuerySelectorAll("div.layout div#infoList div.row");
            JObject roomsJson = new JObject();
            Regex roomAreaReg = new Regex("^([1-9][0-9]*)+(.{0,1}[0-9]{0,2})平米");
            foreach (IElement room in roomList)
            {
                if (roomAreaReg.IsMatch(room.Children[1].TextContent))
                {
                    roomsJson.Add(room.Children[0].TextContent, room.Children[1].TextContent);
                    totalFloorArea += decimal.Parse(room.Children[1].TextContent.Substring(0, room.Children[1].TextContent.Length-2));
                }
                else throw new Exception("The format of room area is not as expected:" + room.ChildNodes[2].TextContent);
            }
            floorAreaDetail = roomsJson.ToString();

            Home home = new Home()
            {
                BuildingTotalFloors = totalFloors,
                ConstructionArea = constructionArea,
                FloorArea = totalFloorArea,
                FloorAreaDetail = floorAreaDetail,
                Bedrooms = bedrooms,
                Bathrooms = bathrooms 
            };

            //totalPrice could not be found on the page when the community is being 限价
            if (totalPrice > 0)
                home.HomeListingPrice = new List<HomeListingPrice> { new HomeListingPrice() { ListingPrice = totalPrice, ListingPriceDate = DateTime.Now.Date } };

            return home;
        }
    }
}
