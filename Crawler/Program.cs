using AngleSharp;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Crawler
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var config = Configuration.Default.WithDefaultLoader();

            var address = "https://xa.ke.com/xiaoqu/qujiang1/";
            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(address);
            // This CSS selector gets the desired content
            var cellSelector = "div.title a.maidian-detail";
            // Perform the query to get all cells with the content
            var cells = document.QuerySelectorAll(cellSelector);

            //var document = await BrowsingContext.New(config).OpenAsync(address);
            // We are only interested in the text - select it with LINQ
            List<string> priceList = new List<string>();
            List<string> building = new List<string>();
            List<string> unitList = new List<string>();

            foreach (var cell in cells)
            {
                var url = cell.GetAttribute("href");
                var documentChild = await BrowsingContext.New(config).OpenAsync(url);
                var priceSelector = "div.xiaoquDescribe span.xiaoquUnitPrice";
                var buildingNumSelector = "div.xiaoquDescribe div.xiaoquInfo div:nth-child(5) span.xiaoquInfoContent";
                var unitsSelector = "div.xiaoquDescribe div.xiaoquInfo div:nth-child(6) span.xiaoquInfoContent";

                var unitsCell = documentChild.QuerySelectorAll(unitsSelector);

            }

            //Console.WriteLine(titles);
        }
    }
}
