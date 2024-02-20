using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
                plotRatioText = document.QuerySelector("div.xiaoquInfo div.xiaoquInfoItem:nth-child(5) span.xiaoquInfoContent").InnerHtml;

                int.TryParse(buildingText.Remove(buildingText.IndexOf('栋')), out buildingNumber);
                int.TryParse(unitsText.Remove(unitsText.IndexOf('户')), out units);
                decimal.TryParse(plotRatioText, out plotRatio);
            }
            catch (Exception e)
            {
                throw new Exception("Unexpected error occurred when parsing data from the community page(" + url + "). The document body is: " + document.Body.InnerHtml, e);
            }

            Community community = new Community();
            community.BuildingNumber = buildingNumber;
            community.Unit = units;
            community.PlotRatio = plotRatio;

            return community;
        }
    }
}
