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
            decimal plotRatio = 0;
            string buildingText = string.Empty;
            string unitsText = string.Empty;
            string homeListURL = string.Empty;
            string plotRatioText = string.Empty;
            IDocument document;

            document = await WebPageReader.GetPageAsync(url);

            try
            {
                buildingText = document.QuerySelector("div.xiaoquInfo div.xiaoquInfoItem:nth-child(3) span.xiaoquInfoContent").InnerHtml;
                unitsText = document.QuerySelector("div.xiaoquInfo div.xiaoquInfoItem:nth-child(2) span.xiaoquInfoContent").InnerHtml;
                homeListURL = document.QuerySelector("div#goodSell a") != null ? document.QuerySelector("div#goodSell a").GetAttribute("href") : string.Empty;
                plotRatioText = document.QuerySelector("div.xiaoquInfo div.xiaoquInfoItem:nth-child(5) span.xiaoquInfoContent").InnerHtml;

                buildingNumber = int.Parse(buildingText.Remove(buildingText.IndexOf('栋')));
                units = int.Parse(unitsText.Remove(unitsText.IndexOf('户')));
                plotRatio = decimal.Parse(plotRatioText);
            }
            catch (Exception e)
            {
                throw new Exception(document.ToString() + "buildingText:" + buildingText + "; unitsText:" + unitsText, e);
            }

            Community community = new Community();
            community.BuildingNumber = buildingNumber;
            community.Unit = units;
            community.HomeListURL = homeListURL;
            community.PlotRatio = plotRatio;

            return community;
        }
    }
}
