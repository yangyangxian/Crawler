using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yang.Entities;
using Yang.Utilities;

namespace SpiderApplication.Seashell.PageHandlers
{
    public class CommunityPageHandler : PageHandler
    {
        //The url will be like 'https://xa.ke.com/xiaoqu/3820028098488153/'
        public static async Task<Community> ReadCommunityDetailData(string url)
        {
            int buildingNumber = 0;
            int units = 0;
            string buildingText = string.Empty;
            string unitsText = string.Empty;
            string homeListURL = string.Empty;
            IDocument document;

            try
            {
                document = await WebPageReader.GetPageAsync(url);

                buildingText = document.QuerySelector("div.xiaoquInfo div:nth-child(5) span.xiaoquInfoContent").InnerHtml;
                unitsText = document.QuerySelector("div.xiaoquInfo div:nth-child(6) span.xiaoquInfoContent").InnerHtml;
                homeListURL = document.QuerySelector("div#goodSell a") != null ? document.QuerySelector("div#goodSell a").GetAttribute("href") : string.Empty;

                buildingNumber = int.Parse(buildingText.Remove(buildingText.IndexOf('栋')));
                units = int.Parse(unitsText.Remove(unitsText.IndexOf('户')));
            }
            catch (Exception e)
            {
                throw new Exception("buildingText:" + buildingText + "; unitsText:" + unitsText, e);
            }

            Community community = new Community();
            community.BuildingNumber = buildingNumber;
            community.Unit = units;
            community.HomeListURL = homeListURL;

            return community;
        }
    }
}
